using Microsoft.AspNetCore.Mvc;
using WebChamado.Models;
using WebChamado.Services;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace WebChamado.Controllers
{
    public class ChamadoController : Controller
    {
        
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
            
            return View(new AbrirChamadoModel());
        }

        [HttpPost]
        
        public async Task<IActionResult> Abrir(AbrirChamadoModel chamado)
        {
            
            if (!ModelState.IsValid)
            {
                return View("AbrirChamado", chamado);
            }

            
            string dataAgoraUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

            chamado.DataAbertura = dataAgoraUtc;
            chamado.DataEncerramento = dataAgoraUtc;

            // CRIAR O CHAMADO NA API
            int? idChamado = await _chamadoService.CriarChamadoAsync(chamado);

            if (idChamado != null)
            {
               
                string respostaIA = await _geminiService.GerarTriagemAsync(chamado.Titulo, chamado.Descricao);

                // SALVAR RESPOSTA DA IA NO BANCO
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