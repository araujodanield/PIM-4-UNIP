using System.ComponentModel.DataAnnotations;
using System;

namespace WebChamado.Models
{
    public class AbrirChamadoModel
    {

        // CAMPOS DO FORMULÁRIO 
        

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(80, ErrorMessage = "O título deve ter no máximo 80 caracteres.")]
        [Display(Name = "Título do Chamado")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(800, ErrorMessage = "A descrição deve ter no máximo 800 caracteres.")]
        [Display(Name = "Descrição Detalhada")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int FkCategoria { get; set; }

        public int FkUsuario { get; set; } = 1;

        [Display(Name = "Prioridade")]
        public int FkPrioridade { get; set; } = 1;

        public int FkStatus { get; set; } = 1;

        public int FkTecnico { get; set; } = 2;

       
        public int FkAvaliacao { get; set; } = 2;

        
        public bool ResolvidoIa { get; set; } = false;

       
        public string ComentarioTecnico { get; set; } = "";

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public string DataEncerramento { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public string DataAbertura { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");


    }
}
