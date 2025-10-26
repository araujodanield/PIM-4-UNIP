using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebChamado.Services
{
    public class RespostaIaApiService
    {
        private readonly HttpClient _httpClient;

        public RespostaIaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        /// Salva a resposta da IA no banco de dados através da API
       
        public async Task<bool> SalvarRespostaAsync(int idChamado, string respostaIA)
        {
            var endpoint = "respostas-ia";

           
            var respostaData = new
            {
                fk_chamado = idChamado,
                resposta = respostaIA,
                data_resposta = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = false
            };

            try
            {
                var jsonContent = JsonSerializer.Serialize(respostaData, options);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                Console.WriteLine($" Salvando resposta IA para chamado #{idChamado}");

                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                Console.WriteLine($" Status ao salvar resposta IA: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($" Resposta IA salva com sucesso!");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($" Erro ao salvar resposta IA: {errorContent}");
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" Erro de conexão ao salvar resposta IA: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Erro ao salvar resposta IA: {ex.Message}");
                return false;
            }
        }
    }
}