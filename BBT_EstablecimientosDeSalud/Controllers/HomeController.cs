using BBT_EstablecimientosDeSalud.Models;
using BBT_EstablecimientosDeSalud.Models.DB;
using BBT_EstablecimientosDeSalud.Repositories;
using BBT_EstablecimientosDeSalud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace BBT_EstablecimientosDeSalud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EstablecimientodeSaludRepositoryimpl estrepo = new EstablecimientodeSaludRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        private readonly EpRepositoryimpl eprepo = new EpRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            EstablecimientoDeSaludViewModel objEstvm = new EstablecimientoDeSaludViewModel();          
            EstablecimientoResponse objResp = new EstablecimientoResponse();
            objEstvm.listEst = estrepo.ListarMap();
            objEstvm.listEps = eprepo.Listar();
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                var idUs = HttpContext.Session.GetString("UsuarioId");
                objEstvm.RecEst = objResp.GetEstablecimiento(Convert.ToInt32(idUs)).Result;
            }
            return View(objEstvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}