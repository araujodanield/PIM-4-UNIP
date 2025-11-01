using PIM4_Desktop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM4_Desktop
{
    public partial class TicketItemControl : UserControl
    {
        private Chamado _chamado;
        public TicketItemControl()
        {
            InitializeComponent();
        }

        public void SetDados(Chamado chamado)
        {
            _chamado = chamado;
            lblTitulo.Text = chamado.Titulo;
            toolTip1.SetToolTip(this.lblTitulo, chamado.Titulo);
            lblUsuario.Text = $"Aberto por: {chamado.UsuarioEmissor}";
            lblData.Text = $"Data e Hora: {chamado.DataAbertura.ToLocalTime():dd/MM/yyyy 'às' HH:mm}";

            // Define a cor do status
            switch (chamado.FkStatus)
            {
                case 1: // "Aberto"
                    panelStatus.BackColor = Color.FromArgb(112, 168, 236);
                    break;
                case 2: // "Em Andamento"
                    panelStatus.BackColor = Color.FromArgb(230, 214, 134);
                    break;
                case 3: // "Manutenção" 
                    panelStatus.BackColor = Color.FromArgb(232, 174, 131);
                    break;
                case 4: // "Finalizado" 
                    panelStatus.BackColor = Color.FromArgb(126, 212, 150);
                    break;
                default:
                    panelStatus.BackColor = Color.FromArgb(181, 181, 181);
                    break;
            }
        }

        private void btnDetalhesTicket_Click(object sender, EventArgs e)
        {
            DetalhesTicket detalhesTicket = new DetalhesTicket(_chamado);
            detalhesTicket.Dock = DockStyle.Fill;
            detalhesTicket.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(detalhesTicket);
            detalhesTicket.Show();
        }
    }
}
