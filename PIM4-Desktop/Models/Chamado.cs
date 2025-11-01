using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4_Desktop.Models
{
    public class Chamado
    {
        // FKs
        [JsonPropertyName("id_chamado")]
        public int IdChamado { get; set; } 
        [JsonPropertyName("fk_usuario")]
        public int FkUsuario { get; set; }
        [JsonPropertyName("fk_categoria")]
        public int FkCategoria { get; set; }
        [JsonPropertyName("fk_prioridade")]
        public int FkPrioridade { get; set; }
        [JsonPropertyName("fk_status")]
        public int FkStatus { get; set; }
        [JsonPropertyName("fk_tecnico")]
        public int? FkTecnico { get; set; }
        [JsonPropertyName("fk_avaliacao")]
        public int? FkAvaliacao { get; set; }

        // Conteúdos
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }
        [JsonPropertyName("resolvido_ia")]
        public bool ResolvidoIA { get; set; }
        [JsonPropertyName("comentario_tecnico")]
        public string? ComentarioTecnico { get; set; }

        // Propriedades com JOINs
        [JsonPropertyName("usuario_emissor")]
        public string UsuarioEmissor { get; set; }
        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }
        [JsonPropertyName("prioridade")]
        public string Prioridade { get; set; }
        [JsonPropertyName("status")]
        public string StatusNome { get; set; }
        [JsonPropertyName("tecnico")]
        public string TecnicoNome { get; set; }
        [JsonPropertyName("avaliacao")]
        public string AvaliacaoDescricao { get; set; }

        // Datas
        [JsonPropertyName("data_abertura")]
        public DateTime DataAbertura { get; set; }
        [JsonPropertyName("data_encerramento")]
        public DateTime? DataEncerramento { get; set; }
    }
}
