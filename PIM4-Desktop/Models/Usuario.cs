using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4_Desktop.Models
{
    public class Usuario
    {
        [JsonPropertyName("id_usuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("fk_tipo_usuario")]
        public int FkTipoUsuario { get; set; }

        [JsonPropertyName("tipo_usuario")]
        public string TipoUsuarioNome { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
