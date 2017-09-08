namespace ConvertItabira
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.opfdArq = new System.Windows.Forms.OpenFileDialog();
            this.btnSelecionaArq = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.rdbSize8 = new System.Windows.Forms.RadioButton();
            this.rdbSize4 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // opfdArq
            // 
            this.opfdArq.Filter = "Arquivo de texto |*.txt";
            this.opfdArq.Title = "Selecione arquivo que será convertido";
            // 
            // btnSelecionaArq
            // 
            this.btnSelecionaArq.Location = new System.Drawing.Point(50, 128);
            this.btnSelecionaArq.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelecionaArq.Name = "btnSelecionaArq";
            this.btnSelecionaArq.Size = new System.Drawing.Size(80, 37);
            this.btnSelecionaArq.TabIndex = 0;
            this.btnSelecionaArq.Text = "Arquivo";
            this.btnSelecionaArq.UseVisualStyleBackColor = true;
            this.btnSelecionaArq.Click += new System.EventHandler(this.btnSelecionaArq_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(198, 129);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 36);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Convert";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // rdbSize8
            // 
            this.rdbSize8.AutoSize = true;
            this.rdbSize8.Location = new System.Drawing.Point(28, 69);
            this.rdbSize8.Name = "rdbSize8";
            this.rdbSize8.Size = new System.Drawing.Size(283, 21);
            this.rdbSize8.TabIndex = 3;
            this.rdbSize8.TabStop = true;
            this.rdbSize8.Text = "8 bytes com Venc. -- Linha 4 posição 46";
            this.rdbSize8.UseVisualStyleBackColor = true;
            // 
            // rdbSize4
            // 
            this.rdbSize4.AutoSize = true;
            this.rdbSize4.Location = new System.Drawing.Point(26, 42);
            this.rdbSize4.Name = "rdbSize4";
            this.rdbSize4.Size = new System.Drawing.Size(283, 21);
            this.rdbSize4.TabIndex = 4;
            this.rdbSize4.TabStop = true;
            this.rdbSize4.Text = "4 bytes sem Venc. -- Linha 4 posição 16";
            this.rdbSize4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 207);
            this.Controls.Add(this.rdbSize4);
            this.Controls.Add(this.rdbSize8);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnSelecionaArq);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert Mult";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opfdArq;
        private System.Windows.Forms.Button btnSelecionaArq;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.RadioButton rdbSize8;
        private System.Windows.Forms.RadioButton rdbSize4;
    }
}

