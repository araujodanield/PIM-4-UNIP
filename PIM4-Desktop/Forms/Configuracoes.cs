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
    public partial class Configuracoes : Form
    {
        public Configuracoes()
        {
            InitializeComponent();
        }

        private void Configuracoes_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Converte o 'sender' para uma label
            Control clickedLabel = sender as Control;
            if (clickedLabel == null) return;

            string mensagem = "Este recurso é meramente ilustrativo e não faz parte das funcionalidades atuais do app.";

            // Calcula a localização para exibir o ToolTip abaixo da label clicada
            Point location = new Point(0, clickedLabel.Height);

            // Exibição e tempo da mensagem
            toolTip2.Show(mensagem, clickedLabel, location, 3000); // 3000ms = 3 segundos
        }

        private void btnLatDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Dock = DockStyle.Fill;
            dashboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(dashboard);
            dashboard.Show();
        }

        private void btnLatDeslogar_Click(object sender, EventArgs e)
        {
            // Pop-up de Confirmação
            string mensagem = "Deseja sair e retornar à tela de login?";
            string titulo = "Confirmação de Saída";
            DialogResult resultado = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificação da resposta
            if (resultado == DialogResult.Yes)
            {
                Login login = new Login();
                login.Dock = DockStyle.Fill;
                login.TopLevel = false;
                MainForm.MainPanel.Controls.Clear();
                MainForm.MainPanel.Controls.Add(login);
                login.Show();
            }
            // Se a resposta for "Não", não faz nada.
        }
    }
}
