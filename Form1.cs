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
            btnConcluir.Click += BtnConcluir_Click;
            listBoxTarefas.SelectedIndexChanged += ListBoxTarefas_SelectedIndexChanged;
            listBoxTarefas.DrawItem += ListBoxTarefas_DrawItem;
            listBoxTarefas.DrawMode = DrawMode.OwnerDrawFixed;
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
                        listBoxTarefas.Items.Add(new TarefaListBoxItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Titulo = reader["Titulo"].ToString(),
                            Status = status,
                            DataConclusao = dataConclusao
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
            if (listBoxTarefas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma tarefa para marcar como concluída.");
                return;
            }

            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null) return;
            int id = item.Id;

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
            var item = listBoxTarefas.SelectedItem as TarefaListBoxItem;
            if (item == null)
                return;

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

        private void ListBoxTarefas_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = listBoxTarefas.Items[e.Index] as TarefaListBoxItem;
            if (item == null) return;

            // Monta o texto: título | (status data)
            string titulo = item.Titulo;
            string statusData = $" ({item.Status}{item.DataConclusao})";

            // Define as cores de fundo e texto conforme seleção
            e.DrawBackground();
            Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? SystemColors.HighlightText
                : listBoxTarefas.ForeColor;

            // Fonte riscada para título se concluída
            Font fontTitulo = item.Status == "Concluída"
                ? new Font(e.Font, FontStyle.Strikeout)
                : e.Font;

            // Calcula o tamanho do título para posicionar o status/data corretamente
            var g = e.Graphics;
            SizeF sizeTitulo = g.MeasureString(titulo, fontTitulo);

            // Desenha o título
            g.DrawString(titulo, fontTitulo, new SolidBrush(textColor), e.Bounds.Left, e.Bounds.Top);

            // Desenha o status/data ao lado do título, sem riscar
            g.DrawString(statusData, e.Font, new SolidBrush(textColor), e.Bounds.Left + sizeTitulo.Width - 5, e.Bounds.Top);

            e.DrawFocusRectangle();
        }
    }
}