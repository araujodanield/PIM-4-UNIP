namespace PIM4_Desktop
{
    partial class TicketItemControl
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
            this.components = new System.ComponentModel.Container();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.btnDetalhesTicket = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picData = new System.Windows.Forms.PictureBox();
            this.picUsuario = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStatus.Location = new System.Drawing.Point(0, 0);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(10, 90);
            this.panelStatus.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.lblTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(261, 16);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "<titulo>";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.lblUsuario.Location = new System.Drawing.Point(40, 42);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(111, 13);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "<nome do usuário>";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.lblData.Location = new System.Drawing.Point(40, 67);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(84, 13);
            this.lblData.TabIndex = 5;
            this.lblData.Text = "<Data e Hora>";
            // 
            // btnDetalhesTicket
            // 
            this.btnDetalhesTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnDetalhesTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetalhesTicket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnDetalhesTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalhesTicket.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalhesTicket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.btnDetalhesTicket.Location = new System.Drawing.Point(201, 37);
            this.btnDetalhesTicket.Margin = new System.Windows.Forms.Padding(0);
            this.btnDetalhesTicket.Name = "btnDetalhesTicket";
            this.btnDetalhesTicket.Size = new System.Drawing.Size(75, 23);
            this.btnDetalhesTicket.TabIndex = 6;
            this.btnDetalhesTicket.Text = "ABRIR";
            this.btnDetalhesTicket.UseVisualStyleBackColor = false;
            this.btnDetalhesTicket.Click += new System.EventHandler(this.btnDetalhesTicket_Click);
            // 
            // picData
            // 
            this.picData.Image = global::PIM4_Desktop.Properties.Resources.icons8_calendar_64;
            this.picData.Location = new System.Drawing.Point(15, 65);
            this.picData.Name = "picData";
            this.picData.Size = new System.Drawing.Size(20, 20);
            this.picData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picData.TabIndex = 4;
            this.picData.TabStop = false;
            // 
            // picUsuario
            // 
            this.picUsuario.Image = global::PIM4_Desktop.Properties.Resources.icons8_user_64;
            this.picUsuario.Location = new System.Drawing.Point(15, 40);
            this.picUsuario.Name = "picUsuario";
            this.picUsuario.Size = new System.Drawing.Size(20, 20);
            this.picUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUsuario.TabIndex = 2;
            this.picUsuario.TabStop = false;
            // 
            // TicketItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Controls.Add(this.btnDetalhesTicket);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.picData);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.picUsuario);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelStatus);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.Name = "TicketItemControl";
            this.Size = new System.Drawing.Size(290, 90);
            ((System.ComponentModel.ISupportInitialize)(this.picData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox picUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.PictureBox picData;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Button btnDetalhesTicket;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
