using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using WebChamado.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebChamado.Services
{
    public class ChamadoApiService
    {
        private readonly HttpClient _httpClient;

        public ChamadoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int?> CriarChamadoAsync(AbrirChamadoModel chamadoData)
        {
            var endpoint = "chamados";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = false
            };

            try
            {
                var jsonContent = JsonSerializer.Serialize(chamadoData, options);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                Console.WriteLine($"📥 Status da API: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"✅ Chamado criado com sucesso!");

                    // 🔧 GAMBIARRA: Buscar o último chamado criado
                    int? ultimoId = await BuscarUltimoChamadoCriadoAsync(chamadoData.FkUsuario);

                    if (ultimoId != null)
                    {
                        Console.WriteLine($"✅ ID do chamado obtido: #{ultimoId}");
                        return ultimoId;
                    }
                    else
                    {
                        Console.WriteLine($"❌ Não foi possível obter o ID do chamado");
                        return null;
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Erro da API: {errorContent}");
                return null;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"❌ Erro de conexão com a API: {httpEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao criar chamado: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 🔧 GAMBIARRA: Busca o último chamado criado pelo usuário
        /// </summary>
        private async Task<int?> BuscarUltimoChamadoCriadoAsync(int idUsuario)
        {
            try
            {
                Console.WriteLine($"🔍 Buscando último chamado do usuário #{idUsuario}...");

                // Espera um pouco para garantir que o banco processou o INSERT
                await Task.Delay(500);

                var response = await _httpClient.GetAsync("chamados");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var chamados = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(responseBody, options);

                    if (chamados != null && chamados.Count > 0)
                    {
                        // Filtra os chamados do usuário e pega o último (maior ID)
                        var chamadosDoUsuario = chamados
                            .Where(c => c.ContainsKey("fk_usuario") && c["fk_usuario"].GetInt32() == idUsuario)
                            .OrderByDescending(c => c.ContainsKey("id_chamado") ? c["id_chamado"].GetInt32() : 0)
                            .ToList();

                        if (chamadosDoUsuario.Any())
                        {
                            var ultimoChamado = chamadosDoUsuario.First();
                            if (ultimoChamado.ContainsKey("id_chamado"))
                            {
                                return ultimoChamado["id_chamado"].GetInt32();
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao buscar último chamado: {ex.Message}");
                return null;
            }
        }

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