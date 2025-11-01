using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4_Desktop.Models
{
    public class Status
    {
        [JsonPropertyName("id_status")]
        public int IdStatus { get; set; }
        [JsonPropertyName("status")]
        public string Nome { get; set; }
    }
}
