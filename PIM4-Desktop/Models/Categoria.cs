using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4_Desktop.Models
{
    public class Categoria
    {
        [JsonPropertyName("id_categoria")]
        public int IdCategoria { get; set; }
        [JsonPropertyName("categoria")]
        public string Nome { get; set; }
    }
}
