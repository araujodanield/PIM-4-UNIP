using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PIM4_Desktop.Services
{
    public static class API
    {
        // URL da API
        private const string BaseUrl = "https://apipim-anfwgmdah3fre6ca.brazilsouth-01.azurewebsites.net/";
        private static readonly HttpClient _httpClient;

        // Execução unica do construtor estático para inicializar o HttpClient
        static API()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public static HttpClient GetClient()
        {
            return _httpClient;
        }
    }
}
