using System;
using System.Data.OleDb;
using System.Drawing;
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
            listBoxTarefas.SelectedIndexChanged += ListBoxTarefas_SelectedIndexChanged;
            listBoxTarefas.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxTarefas.DrawItem += ListBoxTarefas_DrawItem;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnConcluir.Enabled = false; // Mantido desabilitado
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
                      
                        listBoxTarefas.Items.Add(new TarefaListBoxItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Titulo = reader["Titulo"].ToString(),
                            Status = status,
                         
                        });
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar tarefa: " + ex.Message);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null)
            {
                MessageBox.Show("Selecione uma tarefa para excluir.");
                return;
            }
            int id = item.Id;

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

            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null) return;
            int id = item.Id;

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
            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null)
            {
                MessageBox.Show("Selecione uma tarefa para marcar como concluída.");
                return;
            }
            int id = item.Id;

            try
            {
                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new OleDbCommand("UPDATE Tarefas SET Concluida = ? WHERE Id = ?", conn);
                    cmd.Parameters.AddWithValue("?", true); // Apenas altera o status
                    cmd.Parameters.AddWithValue("?", id);
                    cmd.ExecuteNonQuery();
                }
                CarregarTarefas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao concluir tarefa: " + ex.Message);
            }
        }

        private void ListBoxTarefas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null)
            {
                btnExcluir.Enabled = false;
                btnEditar.Enabled = false;
                btnConcluir.Enabled = false;
                return;
            }

            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            // Só habilita se a tarefa estiver pendente
            btnConcluir.Enabled = item.Status == "Pendente";

            int id = item.Id;

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

             

        private void listBoxTarefas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            btnConcluir.Enabled = false;
        }
        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {

        }
        private void btnConcluir_Click_1(object sender, EventArgs e)
        {
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnConcluir.Enabled = false;
            txtDescricao.Clear();
            txtTitulo.Clear();
        }

        private void ListBoxTarefas_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = listBoxTarefas.Items[e.Index] as TarefaListBoxItem;
            if (item == null) return;

            string texto = item.ToString();

            e.DrawBackground();
            Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? SystemColors.HighlightText
                : listBoxTarefas.ForeColor;

            e.Graphics.DrawString(texto, e.Font, new SolidBrush(textColor), e.Bounds.Left, e.Bounds.Top);
            e.DrawFocusRectangle();
        }
    }
}