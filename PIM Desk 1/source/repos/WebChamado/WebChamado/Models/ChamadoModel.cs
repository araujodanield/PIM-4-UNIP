using System.ComponentModel.DataAnnotations;
using System;

namespace WebChamado.Models
{
    public class AbrirChamadoModel
    {

        // ===================================
        // CAMPOS DO FORMULÁRIO (USUÁRIO INSERE)
        // ===================================

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


        // ===================================
        // "GAMBIARRAS" PARA O NOT NULL DO BANCO
        // ===================================

        // 🟢 Status Inicial: 1 (Assumindo que 'Aberto' é o ID 1)
        public int FkStatus { get; set; } = 1;

        // 🟢 Técnico Inicial: 1 (Assumindo que o ID 1 é um valor "nulo" como 'Aguardando Atribuição' ou o seu próprio usuário)
        public int FkTecnico { get; set; } = 2;

        // 🟢 Avaliação Inicial: 1 (Assumindo que o ID 1 é 'Não Avaliado')
        public int FkAvaliacao { get; set; } = 2;

        // 🟢 Resolvido por IA (Deve ser false ao abrir)
        public bool ResolvidoIa { get; set; } = false;

        // 🟢 Comentário Técnico (Pode ser null ou string vazia)
        public string ComentarioTecnico { get; set; } = "";

        // 🟢 Data de Encerramento (Data atual - A API deve ignorar ou validar, mas enviaremos a data atual)
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public string DataEncerramento { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        // 🟢 Data de Abertura (A data em que o chamado é criado)
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public string DataAbertura { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");


    }
}
