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
    public partial class AnalistaItemControl : UserControl
    {
        public AnalistaItemControl()
        {
            InitializeComponent();
        }

        public void SetDados(Usuario analista)
        {
            lblNomeAnalista.Text = analista.Nome;
            lblIdAnalista.Text = $"ID do Analista: {analista.IdUsuario}";
        }
    }
}
