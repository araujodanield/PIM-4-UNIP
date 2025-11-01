using PIM4_Desktop.Models;
using PIM4_Desktop.Services;
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
    public partial class Dashboard : Form
    {
        private readonly ChamadoService _chamadoService = new ChamadoService();
        private const int STATUS_EM_ANDAMENTO_ID = 2;
        private Chamado _chamadoRecenteExibido;

        public Dashboard()
        {
            InitializeComponent();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            // Chama o método para carregar todos os dados
            await CarregarTodosOsDadosAsync();
        }

        private async Task CarregarTodosOsDadosAsync()
        {
            try
            {
                // Inicia as duas chamadas à API em paralelo
                Task<List<Chamado>> chamadosTask = _chamadoService.GetTodosChamadosAsync();
                Task<List<Usuario>> analistasTask = _chamadoService.GetAnalistasAsync();

                // Espera ambas as tarefas serem concluídas
                await Task.WhenAll(chamadosTask, analistasTask);

                // Pega os resultados
                List<Chamado> todosChamados = await chamadosTask;
                List<Usuario> todosAnalistas = await analistasTask;

                // Chama os métodos para preencher cada seção do interface
                PreencherContagemTickets(todosChamados);
                PreencherListaTickets(todosChamados);
                PreencherGraficoStatus(todosChamados);
                PreencherTicketRecente(todosChamados);
                PreencherAnalistas(todosAnalistas);
            }

            catch (Exception ex)
            {
                // Se qualquer chamada falhar, o app inteiro mostra um erro
                MessageBox.Show($"Falha crítica ao carregar os dados do dashboard: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Define todos os campos como "ERRO"
                TotalTickets.Text = "ERRO";
                TotalAnalistas.Text = "ERRO";
            }
        }

        private Color GetColorForStatus(string statusName)
        {
            // Ignorar maiúsculas/minúsculas ao comparar
            if (statusName.Equals("Aberto", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(112, 168, 236);

            if (statusName.Equals("Em Andamento", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(230, 214, 134);

            if (statusName.Equals("Manutenção", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(232, 174, 131);

            if (statusName.Equals("Finalizado", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(126, 212, 150);

            // Retorna uma cor padrão se o status não for encontrado
            return Color.FromArgb(181, 181, 181);
        }

        // Contagem de Tickets
        private void PreencherContagemTickets(List<Chamado> todosChamados)
        {
            TotalTickets.Text = todosChamados.Count.ToString();
        }

        // Lista de Tickets
        private void PreencherListaTickets(List<Chamado> todosChamados)
        {
            flowLayoutPanelTickets.Controls.Clear();
            var chamadosOrdenados = todosChamados.OrderByDescending(c => c.DataAbertura);

            foreach (var chamado in chamadosOrdenados)
            {
                TicketItemControl item = new TicketItemControl();
                item.SetDados(chamado);
                flowLayoutPanelTickets.Controls.Add(item);
            }
        }

        // Gráfico de total de tickets por Status
        private void PreencherGraficoStatus(List<Chamado> todosChamados)
        {
            var contagemPorStatus = todosChamados
                .GroupBy(c => c.StatusNome)

                .Select(grupo => new {
                    Status = grupo.Key,
                    Total = grupo.Count()
                })

                .OrderBy(resultado => resultado.Status)

                .ToList();

            chartTicketStatus.Series.Clear();
            chartTicketStatus.Legends.Clear();
            chartTicketStatus.BackColor = Color.FromArgb(230, 230, 230);
            chartTicketStatus.ChartAreas[0].BackColor = Color.Transparent;

            var series = new System.Windows.Forms.DataVisualization.Charting.Series("TicketsPorStatus")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                LabelForeColor = Color.Black
            };

            foreach (var item in contagemPorStatus)
            {
                int pontoIndex = series.Points.AddXY(item.Status, item.Total);
                var dataPoint = series.Points[pontoIndex];
                dataPoint.Color = GetColorForStatus(item.Status); // Utiliza as mesmas cores de GetColorForStatus
            }

            chartTicketStatus.Series.Add(series);
            chartTicketStatus.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTicketStatus.ChartAreas[0].AxisX.Interval = 1;
            chartTicketStatus.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        }

        // Ticket Recente Lateral
        private void PreencherTicketRecente(List<Chamado> todosChamados)
        {
            // Encontrar e armazenar o ticket mais recente na "memória" da classe
            _chamadoRecenteExibido = todosChamados
                .OrderByDescending(c => c.DataAbertura)
                .FirstOrDefault();

            // Verifica se o ticket foi encontrado
            if (_chamadoRecenteExibido != null)
            {
                // Preenche as labels
                lblRecenteUsuario.Text = _chamadoRecenteExibido.UsuarioEmissor;
                lblRecenteData.Text = $"{_chamadoRecenteExibido.DataAbertura.ToLocalTime():dd/MM/yyyy 'às' HH:mm}";
                lblRecenteTitulo.Text = _chamadoRecenteExibido.Titulo;
                lblRecenteStatus.Text = _chamadoRecenteExibido.StatusNome;

                string descricao = _chamadoRecenteExibido.Descricao;
                int maxChars = 40; // Limite de caracteres para a descrição

                if (string.IsNullOrEmpty(descricao))
                {
                    lblRecenteDescricao.Text = "Nenhuma descrição fornecida.";
                }
                else if (descricao.Length <= maxChars)
                {
                    lblRecenteDescricao.Text = descricao;
                }
                else
                {
                    lblRecenteDescricao.Text = descricao.Substring(0, maxChars) + "...";
                }
            }
            else
            {
                lblRecenteTitulo.Text = "Nenhum ticket encontrado";
            }
        }

        // Lista de Analistas
        private void PreencherAnalistas(List<Usuario> todosAnalistas)
        {
            TotalAnalistas.Text = todosAnalistas.Count.ToString();

            // Preenche a lista
            flowLayoutPanelAnalistas.Controls.Clear();
            var analistasOrdenados = todosAnalistas.OrderBy(a => a.Nome);

            foreach (var analista in analistasOrdenados)
            {
                AnalistaItemControl item = new AnalistaItemControl();
                item.Width = flowLayoutPanelAnalistas.Width - 25;
                item.SetDados(analista);
                flowLayoutPanelAnalistas.Controls.Add(item);
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // Pop-up de Confirmação
            string mensagem = "Deseja sair e retornar à tela de login?";
            string titulo = "Confirmação de Saída";
            DialogResult resultado = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login login = new Login();
                login.Dock = DockStyle.Fill;
                login.TopLevel = false;
                MainForm.MainPanel.Controls.Clear();
                MainForm.MainPanel.Controls.Add(login);
                login.Show();
            }
            // Se for "Não", não faz nada.
        }

        private void btnLatConfigs_Click(object sender, EventArgs e)
        {
            Configuracoes configuracoes = new Configuracoes();
            configuracoes.Dock = DockStyle.Fill;
            configuracoes.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(configuracoes);
            configuracoes.Show();
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            // Verifica se há um ticket recente armazenado
            if (_chamadoRecenteExibido != null)
            {
                // Abre a tela de detalhes com o ticket recente que está sendo exibido
                DetalhesTicket detalhesTicket = new DetalhesTicket(_chamadoRecenteExibido);
                detalhesTicket.Dock = DockStyle.Fill;
                detalhesTicket.TopLevel = false;
                MainForm.MainPanel.Controls.Clear();
                MainForm.MainPanel.Controls.Add(detalhesTicket);
                detalhesTicket.Show();
            }
            else
            {
                MessageBox.Show("Não há ticket para exibir detalhes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}