using System.ComponentModel.DataAnnotations;

namespace WebChamado.Models
{
    public class RespostasIaModel
    {
        [Required]
        public int FkChamado { get; set; }

       
        [Required]
        public string RespostaIA { get; set; } = string.Empty;

        
        public string DataResposta { get; set; } = string.Empty;
    }
}
