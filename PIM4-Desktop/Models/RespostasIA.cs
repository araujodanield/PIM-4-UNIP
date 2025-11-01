using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4_Desktop.Models
{
    public class RespostaIA
    {
        [JsonPropertyName("id_resposta")]
        public int IdResposta { get; set; }

        [JsonPropertyName("fk_chamado")]
        public int FkChamado { get; set; }

        [JsonPropertyName("titulo_chamado")]
        public string TituloChamado { get; set; }

        [JsonPropertyName("resposta")]
        public string Resposta { get; set; }

        [JsonPropertyName("data_resposta")]
        public DateTime DataResposta { get; set; }
    }
}
