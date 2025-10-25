using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace WebChamado.Services
{
    public class GeminiRequest
    {
        public Content[] contents { get; set; } = Array.Empty<Content>();
    }

    public class Content
    {
        public Part[] parts { get; set; } = Array.Empty<Part>();
    }

    public class Part
    {
        public string text { get; set; } = string.Empty;
    }

    public class GeminiResponse
    {
        public Candidate[] candidates { get; set; } = Array.Empty<Candidate>();
    }

    public class Candidate
    {
        public Content content { get; set; } = new Content();
    }

    public class GeminiApiService
    {
        private readonly HttpClient _httpClient;
        private const string GeminiApiKey = "AIzaSyBDUVP2ygNwA1cSf5QcKPloyAYMmhZJ-uo";

        public GeminiApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GerarTriagemAsync(string titulo, string descricao)
        {
            // O PROMPT: Instruções claras para a IA sobre sua função de triagem
            string prompt = $@"
      Você é uma IA de triagem de help desk corporativo. Forneça uma resposta técnica inicial para o chamado de um funcionário.

REGRAS IMPORTANTES:
1. Trate o usuário como funcionário de uma empresa (ambiente corporativo)
2. Não sugira soluções que exijam abertura física de equipamentos
3. Esta será a ÚNICA resposta automática - não mencione 'entre em contato novamente'
4. Seja objetivo, profissional e respeitoso
5. Respeite os direitos humanos: não discrimine, não ofenda, seja inclusivo
6. Limite: máximo 200 caracteres
7. NÃO use formatação markdown (**, ##, etc)
8. NÃO identifique o tipo de problema no início da resposta
9. Vá direto para as soluções

FORMATO DA RESPOSTA:
- Comece diretamente com as ações que o funcionário deve tomar
- Numere as ações (1., 2., etc)
- Use frases curtas e claras
- Uma ação por linha
            ";

            // 1. Cria o payload JSON que será enviado
            var requestData = new GeminiRequest
            {
                contents = new[]
                {
                    new Content { parts = new[] { new Part { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 2. Define o endpoint final: modelo (gemini-2.5-flash) + chave API
            string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={GeminiApiKey}";

            try
            {
                Console.WriteLine("Gerando triagem com Gemini...");
                // 3. Envia a requisição POST
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                // Se o status HTTP não for 2xx (ex: 400 Bad Request ou 500 Server Error), lança exceção
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // 4. Desserializa a resposta JSON
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseBody, options);

                // 5. Extrai o texto da resposta
                if (geminiResponse?.candidates?.Length > 0 && geminiResponse.candidates[0].content.parts.Length > 0)
                {
                    return geminiResponse.candidates[0].content.parts[0].text.Trim();
                }

                return "Erro: A API do Gemini não retornou texto válido. O chamado será escalonado para revisão.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na API do Gemini: {ex.Message}");
                // Retorna uma resposta segura em caso de falha na comunicação
                return "Não foi possível realizar a triagem automática. O chamado será revisado por um técnico.";
            }
        }
    }
}