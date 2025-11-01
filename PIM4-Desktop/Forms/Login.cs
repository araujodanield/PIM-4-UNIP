using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM4_Desktop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Move o foco inicial para um elemento neutro
            this.ActiveControl = labelLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Dock = DockStyle.Fill;
            dashboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(dashboard);
            dashboard.Show();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            // Se o texto for o placeholder, apague-o e mude a cor para preto
            if (txtUsername.Text == "Usuário")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            // Se o campo estiver vazio quando o usuário trocar, restaure o placeholder
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Usuário";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Senha")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;

                // Ativa a proteção de digitação de senha
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            // Desativa a proteção de digitação de senha e restaura o placeholder
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Senha";
                txtPassword.ForeColor = Color.Gray;
            }
        }
    }
}
