
namespace ExemploEmissaoNFe
{
    partial class frmNFe
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
            if (disposing && (components != null))
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
            this.btnEnviarNota = new System.Windows.Forms.Button();
            this.btnCancelamento = new System.Windows.Forms.Button();
            this.btnCCe = new System.Windows.Forms.Button();
            this.btnIuntilizacao = new System.Windows.Forms.Button();
            this.btnConsultaCadastroContrib = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnviarNota
            // 
            this.btnEnviarNota.Location = new System.Drawing.Point(28, 12);
            this.btnEnviarNota.Name = "btnEnviarNota";
            this.btnEnviarNota.Size = new System.Drawing.Size(134, 53);
            this.btnEnviarNota.TabIndex = 0;
            this.btnEnviarNota.Text = "Enviar Nota";
            this.btnEnviarNota.UseVisualStyleBackColor = true;
            this.btnEnviarNota.Click += new System.EventHandler(this.btnEnviarNota_Click);
            // 
            // btnCancelamento
            // 
            this.btnCancelamento.Location = new System.Drawing.Point(28, 80);
            this.btnCancelamento.Name = "btnCancelamento";
            this.btnCancelamento.Size = new System.Drawing.Size(134, 54);
            this.btnCancelamento.TabIndex = 1;
            this.btnCancelamento.Text = "button1";
            this.btnCancelamento.UseVisualStyleBackColor = true;
            this.btnCancelamento.Click += new System.EventHandler(this.btnCancelamento_Click);
            // 
            // btnCCe
            // 
            this.btnCCe.Location = new System.Drawing.Point(202, 45);
            this.btnCCe.Name = "btnCCe";
            this.btnCCe.Size = new System.Drawing.Size(95, 38);
            this.btnCCe.TabIndex = 2;
            this.btnCCe.Text = "button1";
            this.btnCCe.UseVisualStyleBackColor = true;
            this.btnCCe.Click += new System.EventHandler(this.btnCCe_Click);
            // 
            // btnIuntilizacao
            // 
            this.btnIuntilizacao.Location = new System.Drawing.Point(204, 107);
            this.btnIuntilizacao.Name = "btnIuntilizacao";
            this.btnIuntilizacao.Size = new System.Drawing.Size(93, 27);
            this.btnIuntilizacao.TabIndex = 3;
            this.btnIuntilizacao.Text = "button1";
            this.btnIuntilizacao.UseVisualStyleBackColor = true;
            this.btnIuntilizacao.Click += new System.EventHandler(this.btnIuntilizacao_Click);
            // 
            // btnConsultaCadastroContrib
            // 
            this.btnConsultaCadastroContrib.Location = new System.Drawing.Point(202, 158);
            this.btnConsultaCadastroContrib.Name = "btnConsultaCadastroContrib";
            this.btnConsultaCadastroContrib.Size = new System.Drawing.Size(93, 27);
            this.btnConsultaCadastroContrib.TabIndex = 4;
            this.btnConsultaCadastroContrib.Text = "button1";
            this.btnConsultaCadastroContrib.UseVisualStyleBackColor = true;
            this.btnConsultaCadastroContrib.Click += new System.EventHandler(this.btnConsultaCadastroContrib_Click);
            // 
            // frmNFe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 242);
            this.Controls.Add(this.btnConsultaCadastroContrib);
            this.Controls.Add(this.btnIuntilizacao);
            this.Controls.Add(this.btnCCe);
            this.Controls.Add(this.btnCancelamento);
            this.Controls.Add(this.btnEnviarNota);
            this.Name = "frmNFe";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmNFe_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnviarNota;
        private System.Windows.Forms.Button btnCancelamento;
        private System.Windows.Forms.Button btnCCe;
        private System.Windows.Forms.Button btnIuntilizacao;
        private System.Windows.Forms.Button btnConsultaCadastroContrib;
    }
}

