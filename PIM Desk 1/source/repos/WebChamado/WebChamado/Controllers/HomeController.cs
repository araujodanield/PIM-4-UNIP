using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebChamado.Models;

namespace WebChamado.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Página inicial
        public IActionResult Index()
        {
            return View();
        }

        // Página para visualizar informações gerais (ou outra funcionalidade)
        public IActionResult Visualizar()
        {
            return View();
        }

        // Página de erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

