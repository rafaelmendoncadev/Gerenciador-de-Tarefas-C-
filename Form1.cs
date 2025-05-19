using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=GerenciadorTarefas.accdb;";

        public Form1()
        {
            InitializeComponent();
            CarregarTarefas();
            btnAdicionar.Click += BtnAdicionar_Click;
            btnExcluir.Click += BtnExcluir_Click;
            btnEditar.Click += BtnEditar_Click;
            btnConcluir.Click += BtnConcluir_Click;
            listBoxTarefas.SelectedIndexChanged += ListBoxTarefas_SelectedIndexChanged;
        }

        private void CarregarTarefas()
        {
            listBoxTarefas.Items.Clear();
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT Id, Titulo, Concluida, DataConclusao FROM Tarefas", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string status = Convert.ToBoolean(reader["Concluida"]) ? "Concluída" : "Pendente";
                        string dataConclusao = reader["DataConclusao"] != DBNull.Value
                            ? $" - {Convert.ToDateTime(reader["DataConclusao"]).ToShortDateString()}"
                            : "";
                        listBoxTarefas.Items.Add($"{reader["Id"]} - {reader["Titulo"]} ({status}{dataConclusao})");
                    }
                }
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Informe o título da tarefa.");
                return;
            }

            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("INSERT INTO Tarefas (Titulo, Descricao, Concluida) VALUES (?, ?, ?)", conn);
                cmd.Parameters.AddWithValue("?", txtTitulo.Text);
                cmd.Parameters.AddWithValue("?", txtDescricao.Text);
                cmd.Parameters.AddWithValue("?", false);
                cmd.ExecuteNonQuery();
            }

            txtTitulo.Clear();
            txtDescricao.Clear();
            CarregarTarefas();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (listBoxTarefas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma tarefa para excluir.");
                return;
            }

            var item = listBoxTarefas.SelectedItem.ToString();
            int id = int.Parse(item.Split('-')[0].Trim());

            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("DELETE FROM Tarefas WHERE Id = ?", conn);
                cmd.Parameters.AddWithValue("?", id);
                cmd.ExecuteNonQuery();
            }

            txtTitulo.Clear();
            txtDescricao.Clear();
            CarregarTarefas();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (listBoxTarefas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma tarefa para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Informe o título da tarefa.");
                return;
            }

            var item = listBoxTarefas.SelectedItem.ToString();
            int id = int.Parse(item.Split('-')[0].Trim());

            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("UPDATE Tarefas SET Titulo = ?, Descricao = ? WHERE Id = ?", conn);
                cmd.Parameters.AddWithValue("?", txtTitulo.Text);
                cmd.Parameters.AddWithValue("?", txtDescricao.Text);
                cmd.Parameters.AddWithValue("?", id);
                cmd.ExecuteNonQuery();
            }

            txtTitulo.Clear();
            txtDescricao.Clear();
            CarregarTarefas();
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (listBoxTarefas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma tarefa para marcar como concluída.");
                return;
            }

            var item = listBoxTarefas.SelectedItem.ToString();
            if (!int.TryParse(item.Split('-')[0].Trim(), out int id))
            {
                MessageBox.Show("Id da tarefa inválido.");
                return;
            }

            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("UPDATE Tarefas SET Concluida = ?, DataConclusao = ? WHERE Id = ?", conn);
                cmd.Parameters.AddWithValue("?", -1); // -1 para true no Access
                cmd.Parameters.AddWithValue("?", DateTime.Now);
                cmd.Parameters.AddWithValue("?", id); // id deve ser inteiro
                cmd.ExecuteNonQuery();
            }

            CarregarTarefas();
        }
        private void ListBoxTarefas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTarefas.SelectedItem == null)
                return;

            var item = listBoxTarefas.SelectedItem.ToString();
            int id = int.Parse(item.Split('-')[0].Trim());

            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT Titulo, Descricao FROM Tarefas WHERE Id = ?", conn);
                cmd.Parameters.AddWithValue("?", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtTitulo.Text = reader["Titulo"].ToString();
                        txtDescricao.Text = reader["Descricao"].ToString();
                    }
                }
            }
        }
    }
}