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
    public partial class MainForm : Form
    {
        public static Panel MainPanel;
        public MainForm()
        {
            InitializeComponent();
            MainPanel = panel1;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Dock = DockStyle.Fill;
            login.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(login);
            login.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Pop-up de Confirmação
            string mensagem = "Deseja encerrar a aplicação?";
            string titulo = "Confirmação de Encerramento";
            DialogResult resultado = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.No)
            {
                // Se for "Não", cancela o fechamento
                e.Cancel = true;
            }
            // Se for "Sim", continua o processo de fechar.
        }
    }
}
