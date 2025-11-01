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
    public partial class DetalhesTicket : Form
    {
        private Chamado _chamado;
        private readonly ChamadoService _chamadoService = new ChamadoService();

        private ChamadoUpdate MapearParaChamadoUpdate(Chamado chamadoOriginal)
        {
            return new ChamadoUpdate
            {
                FkUsuario = chamadoOriginal.FkUsuario,
                FkCategoria = chamadoOriginal.FkCategoria,
                FkPrioridade = chamadoOriginal.FkPrioridade,
                FkStatus = chamadoOriginal.FkStatus,
                FkTecnico = chamadoOriginal.FkTecnico,
                FkAvaliacao = chamadoOriginal.FkAvaliacao,

                // Conteúdo
                Titulo = chamadoOriginal.Titulo,
                Descricao = chamadoOriginal.Descricao,
                ResolvidoIA = chamadoOriginal.ResolvidoIA,
                ComentarioTecnico = chamadoOriginal.ComentarioTecnico,

                // Datas
                DataAbertura = chamadoOriginal.DataAbertura,
                DataEncerramento = chamadoOriginal.DataEncerramento 
            };
        }

        public DetalhesTicket(Chamado chamado)
        {
            InitializeComponent();
            _chamado = chamado;
            CarregarDetalhes();
            CarregarRespostaIA();
        }

        private void CarregarDetalhes()
        {
            if (_chamado == null) return;

            label2.Text = _chamado.UsuarioEmissor;
            label4.Text = _chamado.Categoria;
            label8.Text = _chamado.StatusNome;

            // Título com hover
            label11.Text = _chamado.Titulo;
            toolTip1.SetToolTip(this.label11, _chamado.Titulo); // Define o hover

            // Descrição
            descricaoChamado.Text = _chamado.Descricao;
            label13.Text = $"{_chamado.DataAbertura.ToLocalTime():dd/MM/yyyy 'às' HH:mm}";
            label17.Text = _chamado.IdChamado.ToString();

            // Seletor de status
            statusComboBox.SelectedItem = _chamado.StatusNome;
            if (_chamado.FkStatus == 4)
            {
                statusComboBox.Enabled = false;
                statusComboBox.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                statusComboBox.Enabled = true;
                statusComboBox.BackColor = System.Drawing.SystemColors.Window;
            }

            AtualizarMensagemFinalizado();
            DetalhesTicket_Resize(null, null);
        }

        private async void CarregarRespostaIA()
        {
            if (_chamado == null) return;
            try
            {
                List<RespostaIA> respostas = await _chamadoService.GetRespostasIAAsync(_chamado.IdChamado);

                if (respostas != null && respostas.Count > 0)
                {
                    mensagemIA.Text = respostas[0].Resposta;
                    horaEnvioIA.Text = $"{respostas[0].DataResposta.ToLocalTime():dd/MM/yyyy 'às' HH:mm}";
                }
                else
                {
                    mensagemIA.Text = "Nenhuma resposta da IA encontrada para este chamado.";
                    horaEnvioIA.Text = "-";
                }
            }
            catch (Exception ex)
            {
                mensagemIA.Text = "Erro ao carregar resposta da IA.";
                horaEnvioIA.Text = "-";
                MessageBox.Show($"Erro ao buscar resposta da IA: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DetalhesTicket_Resize(null, null);
            }
        }

        private void DetalhesTicket_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLatDeslogar_Click(object sender, EventArgs e)
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

        private void btnLatDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Dock = DockStyle.Fill;
            dashboard.TopLevel = false;
            MainForm.MainPanel.Controls.Clear();
            MainForm.MainPanel.Controls.Add(dashboard);
            dashboard.Show();
        }

        private void exitDetails_Click(object sender, EventArgs e)
        {
            btnLatDashboard_Click(sender, e);
        }

        private void AtualizarMensagemFinalizado()
        {
            // Verifica se o status é Finalizado (ID 4) e se a data de encerramento existe
            if (_chamado.FkStatus == 4 && _chamado.DataEncerramento.HasValue)
            {
                // Se sim, formata a mensagem e torna a label visível 
                lblMensagemFinalizado.Text = $"O chamado foi finalizado em {_chamado.DataEncerramento.Value.ToLocalTime():dd/MM/yyyy} às {_chamado.DataEncerramento.Value.ToLocalTime():HH:mm}";
                lblMensagemFinalizado.Visible = true;
            }
            else if (_chamado.FkStatus == 2)
            {
                // Se estiver "Em Andamento", mostra uma mensagem informando que o usuário não resolveu com a IA
                lblMensagemFinalizado.Text = "O usuário não resolveu o problema com a IA e está aguardando um analista.";
                lblMensagemFinalizado.Visible = true;
            }
            else
            {
                // Mantém a label oculta 
                lblMensagemFinalizado.Visible = false;
            }
        }

        private async void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Garante que algo foi selecionado e que não é o status que já está ativo
            if (statusComboBox.SelectedItem == null || statusComboBox.SelectedItem.ToString() == _chamado.StatusNome)
                return;

            string novoStatusTexto = statusComboBox.SelectedItem.ToString();

            // Confirmação de troca
            string mensagem = $"Deseja alterar o status do chamado para \"{novoStatusTexto}\"?";
            DialogResult resultado = MessageBox.Show(mensagem, "Confirmação de Alteração", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Se o utilizador clicar em "Não", a mudança visual na ComboBox é revertida.
            if (resultado == DialogResult.No)
            {
                statusComboBox.SelectedItem = _chamado.StatusNome;
                return;
            }

            int novoStatusId = 0;

            if (novoStatusTexto == "Manutenção")
            {
                novoStatusId = 3;
            }
            else if (novoStatusTexto == "Finalizado")
            {
                novoStatusId = 4;
            }

            // Segunda verificação de mudança para evitar chamadas desnecessárias
            if (novoStatusId > 0 && novoStatusId != _chamado.FkStatus)
            {
                try
                {
                    ChamadoUpdate updateModel = MapearParaChamadoUpdate(_chamado);
                    updateModel.FkStatus = novoStatusId;

                    if (novoStatusId == 4)
                    {
                        updateModel.DataEncerramento = DateTime.Now;
                    }

                    bool sucesso = await _chamadoService.AtualizarChamadoAsync(_chamado.IdChamado, updateModel);

                    if (sucesso)
                    {
                        _chamado.FkStatus = novoStatusId;
                        _chamado.StatusNome = novoStatusTexto;

                        if (updateModel.DataEncerramento.HasValue)
                        {
                            _chamado.DataEncerramento = updateModel.DataEncerramento;
                        }

                        label8.Text = novoStatusTexto;

                        AtualizarMensagemFinalizado();

                        if (novoStatusId == 4)
                        {
                            statusComboBox.Enabled = false;
                            statusComboBox.BackColor = System.Drawing.SystemColors.Control;
                        }
                        MessageBox.Show("Status atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Falha ao atualizar o status na API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        statusComboBox.SelectedItem = _chamado.StatusNome;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro de processamento: {ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusComboBox.SelectedItem = _chamado.StatusNome;
                }
            }
        }

        private void DetalhesTicket_Resize(object sender, EventArgs e)
        {
            Form mainForm = this.FindForm();
            if (mainForm == null) return;

            int availableWidth = panel5.ClientSize.Width - 10; // -10 para uma pequena margem
            if (availableWidth > 200) availableWidth = 510;

            descricaoChamado.MaximumSize = new Size(590, 0); // 0 = altura ilimitada
            mensagemIA.MaximumSize = new Size(500, 0);

            // FullScreen
            if (mainForm.WindowState == FormWindowState.Maximized)
            {
                label11.AutoEllipsis = false;
                label11.MaximumSize = new Size(availableWidth, 0);
            }
            else // Janela
            {
                label11.AutoEllipsis = true;
                label11.MaximumSize = new Size(availableWidth, 90);
            }
        }
    }
}
