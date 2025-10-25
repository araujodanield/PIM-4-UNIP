using Microsoft.AspNetCore.Mvc;
using WebChamado.Models;
using WebChamado.Services;
using System.Threading.Tasks;
using System.Linq;
using System; // 🟢 NOVO: Necessário para usar DateTime

namespace WebChamado.Controllers
{
    public class ChamadoController : Controller
    {
        // 🟢 AÇÃO 1: DECLARAR E INJETAR OS SERVIÇOS
        private readonly ChamadoApiService _chamadoService;
        private readonly GeminiApiService _geminiService;
        private readonly RespostaIaApiService _respostaIaService;

        public ChamadoController(
            ChamadoApiService chamadoService,
            GeminiApiService geminiService,
            RespostaIaApiService respostaIaService)
        {
            _chamadoService = chamadoService;
            _geminiService = geminiService;
            _respostaIaService = respostaIaService;
        }

        public IActionResult AbrirChamado()
        {
            // 🟢 BOA PRÁTICA: Envia um modelo limpo para a View
            return View(new AbrirChamadoModel());
        }

        [HttpPost]
        // 🟢 AÇÃO 2: MÉTODO ASSÍNCRONO
        public async Task<IActionResult> Abrir(AbrirChamadoModel chamado)
        {
            // 🟢 AÇÃO 3: Retorna para a View com o modelo se a validação falhar
            if (!ModelState.IsValid)
            {
                return View("AbrirChamado", chamado);
            }

            // 🟢 AÇÃO 4: CORRIGIR AS DATAS E FUSO HORÁRIO (A "gambiarra" segura)
            string dataAgoraUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

            chamado.DataAbertura = dataAgoraUtc;
            chamado.DataEncerramento = dataAgoraUtc;

            // 🟢 AÇÃO 5: CRIAR O CHAMADO NA API
            int? idChamado = await _chamadoService.CriarChamadoAsync(chamado);

            if (idChamado != null)
            {
               
                string respostaIA = await _geminiService.GerarTriagemAsync(chamado.Titulo, chamado.Descricao);

                // 💾 AÇÃO 7: SALVAR RESPOSTA DA IA NO BANCO
                await _respostaIaService.SalvarRespostaAsync(idChamado.Value, respostaIA);

                return RedirectToAction("TicketEnviado");
            }
            else
            {
                ModelState.AddModelError("", "Falha ao criar o chamado. Verifique a API ou tente novamente.");
                return View("AbrirChamado", chamado);
            }
        }

        // Página de confirmação
        public IActionResult TicketEnviado()
        {
            return View();
        }

    }
}