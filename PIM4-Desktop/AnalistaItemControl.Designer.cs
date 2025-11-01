namespace PIM4_Desktop
{
    partial class AnalistaItemControl
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNomeAnalista = new System.Windows.Forms.Label();
            this.lblIdAnalista = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeAnalista
            // 
            this.lblNomeAnalista.AutoSize = true;
            this.lblNomeAnalista.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeAnalista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.lblNomeAnalista.Location = new System.Drawing.Point(32, 19);
            this.lblNomeAnalista.Name = "lblNomeAnalista";
            this.lblNomeAnalista.Size = new System.Drawing.Size(133, 17);
            this.lblNomeAnalista.TabIndex = 0;
            this.lblNomeAnalista.Text = "<nome do analista>";
            // 
            // lblIdAnalista
            // 
            this.lblIdAnalista.AutoSize = true;
            this.lblIdAnalista.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdAnalista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.lblIdAnalista.Location = new System.Drawing.Point(13, 58);
            this.lblIdAnalista.Name = "lblIdAnalista";
            this.lblIdAnalista.Size = new System.Drawing.Size(77, 13);
            this.lblIdAnalista.TabIndex = 1;
            this.lblIdAnalista.Text = "<ID analista>";
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStatus.Location = new System.Drawing.Point(0, 0);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(10, 90);
            this.panelStatus.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PIM4_Desktop.Properties.Resources.icons8_fone_de_ouvido_64;
            this.pictureBox1.Location = new System.Drawing.Point(16, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 17);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // AnalistaItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.lblIdAnalista);
            this.Controls.Add(this.lblNomeAnalista);
            this.Name = "AnalistaItemControl";
            this.Size = new System.Drawing.Size(300, 90);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeAnalista;
        private System.Windows.Forms.Label lblIdAnalista;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
