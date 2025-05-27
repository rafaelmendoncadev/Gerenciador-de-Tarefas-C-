namespace Gerenciador_de_Tarefas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtTitulo = new TextBox();
            btnAdicionar = new Button();
            btnEditar = new Button();
            btnExcluir = new Button();
            listBoxTarefas = new ListBox();
            txtDescricao = new TextBox();
            btnConcluir = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtTitulo
            // 
            txtTitulo.BackColor = SystemColors.Info;
            txtTitulo.Location = new Point(46, 91);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.PlaceholderText = "Título da Tarefa";
            txtTitulo.Size = new Size(306, 23);
            txtTitulo.TabIndex = 0;
            // 
            // btnAdicionar
            // 
            btnAdicionar.BackColor = SystemColors.Info;
            btnAdicionar.Location = new Point(41, 383);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(72, 23);
            btnAdicionar.TabIndex = 1;
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.UseVisualStyleBackColor = false;
            btnAdicionar.Click += btnAdicionar_Click_1;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = SystemColors.Info;
            btnEditar.Location = new Point(122, 383);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(72, 23);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnExcluir
            // 
            btnExcluir.BackColor = SystemColors.Info;
            btnExcluir.Location = new Point(203, 383);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(72, 23);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = false;
            // 
            // listBoxTarefas
            // 
            listBoxTarefas.BackColor = SystemColors.Info;
            listBoxTarefas.FormattingEnabled = true;
            listBoxTarefas.ItemHeight = 15;
            listBoxTarefas.Location = new Point(46, 183);
            listBoxTarefas.Name = "listBoxTarefas";
            listBoxTarefas.Size = new Size(306, 184);
            listBoxTarefas.TabIndex = 4;
            listBoxTarefas.SelectedIndexChanged += listBoxTarefas_SelectedIndexChanged_1;
            // 
            // txtDescricao
            // 
            txtDescricao.BackColor = SystemColors.Info;
            txtDescricao.Location = new Point(46, 142);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.PlaceholderText = "Descrição da Tarefa";
            txtDescricao.Size = new Size(306, 23);
            txtDescricao.TabIndex = 5;
            // 
            // btnConcluir
            // 
            btnConcluir.BackColor = SystemColors.Info;
            btnConcluir.Location = new Point(284, 383);
            btnConcluir.Name = "btnConcluir";
            btnConcluir.Size = new Size(72, 23);
            btnConcluir.TabIndex = 6;
            btnConcluir.Text = "Concluir";
            btnConcluir.UseVisualStyleBackColor = false;
            btnConcluir.Click += btnConcluir_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            label1.Location = new Point(249, 9);
            label1.Name = "label1";
            label1.Size = new Size(291, 41);
            label1.TabIndex = 7;
            label1.Text = "Controle de Tarefas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(46, 73);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 8;
            label2.Text = "Título";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(46, 124);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 9;
            label3.Text = "Descrição";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Gerenciador_de_Tarefa;
            pictureBox1.Location = new Point(444, 91);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(300, 300);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.MediumAquamarine;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnConcluir);
            Controls.Add(txtDescricao);
            Controls.Add(listBoxTarefas);
            Controls.Add(btnExcluir);
            Controls.Add(btnEditar);
            Controls.Add(btnAdicionar);
            Controls.Add(txtTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Controle de Tarefas";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitulo;
        private Button btnAdicionar;
        private Button btnEditar;
        private Button btnExcluir;
        private ListBox listBoxTarefas;
        private TextBox txtDescricao;
        private Button btnConcluir;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox1;
    }
}
