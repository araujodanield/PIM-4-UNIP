using System.Net.Http.Json;
using PIM4_Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace PIM4_Desktop.Services
{
    public class ChamadoService
    {
        private readonly HttpClient _client;
        public ChamadoService()
        {
            // Pega a instância única do HttpClient
            _client = API.GetClient();
        }

        // Método para listar todos os chamados (para o Dashboard)
        public async Task<List<Chamado>> GetTodosChamadosAsync()
        {
            try
            {
                // Chamar a URL
                var chamados = await _client.GetFromJsonAsync<List<Chamado>>("api/chamados");
                return chamados ?? new List<Chamado>();
            }
            catch (Exception ex)
            {
                // Mensagem de erro
                Console.WriteLine($"Erro ao buscar todos os chamados: {ex.Message}");
                return new List<Chamado>();
            }
        }

        // Método para buscar um chamado específico por ID (para DetalhesTicket)
        public async Task<Chamado> GetChamadoPorIdAsync(int id)
        {
            try
            {
                var chamado = await _client.GetFromJsonAsync<Chamado>($"api/chamados/{id}");
                return chamado; // Pode retornar null se não for encontrado
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar chamado {id}: {ex.Message}");
                return null;
            }
        }

        // Método para buscar o usuário
        public async Task<Usuario> GetUsuarioPorIdAsync(int idUsuario)
        {
            try
            {
                var usuario = await _client.GetFromJsonAsync<Usuario>($"api/usuarios/{idUsuario}");
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário {idUsuario}: {ex.Message}");
                return null;
            }
        }

        // Método para buscar as respostas da IA
        public async Task<List<RespostaIA>> GetRespostasIAAsync(int idChamado)
        {
            try
            {
                // Filtra as respostas da IA pelo ID do chamado
                var todasRespostas = await _client.GetFromJsonAsync<List<RespostaIA>>("api/respostas-ia");
                return todasRespostas?.Where(r => r.FkChamado == idChamado).ToList() ?? new List<RespostaIA>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar respostas IA para chamado {idChamado}: {ex.Message}");
                return new List<RespostaIA>();
            }
        }

        public async Task<List<Usuario>> GetTodosUsuariosAsync()
        {
            try
            {
                var usuarios = await _client.GetFromJsonAsync<List<Usuario>>("api/usuarios");
                return usuarios ?? new List<Usuario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os usuários: {ex.Message}");
                return new List<Usuario>();
            }
        }

        // Método para filtrar os analistas
        public async Task<List<Usuario>> GetAnalistasAsync()
        {
            var todosUsuarios = await GetTodosUsuariosAsync();
            string TIPO_ANALISTA = "Técnico";

            return todosUsuarios
                .Where(u => u.TipoUsuarioNome.Equals(TIPO_ANALISTA, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Método para atualizar o chamado
        public async Task<bool> AtualizarChamadoAsync(int id, ChamadoUpdate updateModel)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"api/chamados/{id}", updateModel);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha na conexão ao atualizar chamado {id}: {ex.Message}");
                return false;
            }
        }
    }
}
