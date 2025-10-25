using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using WebChamado.Models;

namespace WebChamado.Services
{
    public class ChamadoApiService
    {
        private readonly HttpClient _httpClient;

        // ✅ O HttpClient é injetado e já vem configurado pelo Program.cs
        // com BaseAddress e Headers apropriados
        public ChamadoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Cria um novo chamado na API
        /// </summary>
        /// <param name="chamadoData">Dados do chamado a ser criado</param>
        /// <returns>True se o chamado foi criado com sucesso, False caso contrário</returns>
        public async Task<bool> CriarChamadoAsync(AbrirChamadoModel chamadoData)
        {
            var endpoint = "chamados";

            // Configuração para converter PascalCase para snake_case
            // Exemplo: FkUsuario -> fk_usuario
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = false
            };

            try
            {
                // 1. Serializa o objeto C# para JSON
                var jsonContent = JsonSerializer.Serialize(chamadoData, options);

                // 🔍 Log para debug (opcional - remova em produção)
                Console.WriteLine($"📤 Enviando para API: {jsonContent}");

                // 2. Cria o StringContent para enviar
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 3. Envia a requisição POST para /api/chamados
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                // 🔍 Log da resposta (opcional - remova em produção)
                Console.WriteLine($"📥 Status da API: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Erro da API: {errorContent}");
                }

                // 4. Retorna true se for 2xx (200 OK, 201 Created, etc)
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpEx)
            {
                // Erro de conexão/rede
                Console.WriteLine($"❌ Erro de conexão com a API: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Outros erros
                Console.WriteLine($"❌ Erro ao criar chamado: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Método opcional para testar a conexão com a API
        /// </summary>
        public async Task<bool> TestarConexaoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("chamados");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}