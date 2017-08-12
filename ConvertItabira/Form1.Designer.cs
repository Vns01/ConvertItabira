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
            this.SuspendLayout();
            // 
            // opfdArq
            // 
            this.opfdArq.Filter = "Arquivo de texto |*.txt";
            this.opfdArq.Title = "Selecione arquivo que será convertido";
            // 
            // btnSelecionaArq
            // 
            this.btnSelecionaArq.Location = new System.Drawing.Point(43, 75);
            this.btnSelecionaArq.Name = "btnSelecionaArq";
            this.btnSelecionaArq.Size = new System.Drawing.Size(60, 29);
            this.btnSelecionaArq.TabIndex = 0;
            this.btnSelecionaArq.Text = "Arquivo";
            this.btnSelecionaArq.UseVisualStyleBackColor = true;
            this.btnSelecionaArq.Click += new System.EventHandler(this.btnSelecionaArq_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(172, 75);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(60, 29);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Convert";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 180);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnSelecionaArq);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert Itabira";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opfdArq;
        private System.Windows.Forms.Button btnSelecionaArq;
        private System.Windows.Forms.Button btnSalvar;
    }
}

