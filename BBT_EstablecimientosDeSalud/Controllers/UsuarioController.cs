using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBT_EstablecimientosDeSalud.Models;
using BBT_EstablecimientosDeSalud.Repositories;

namespace BBT_EstablecimientosDeSalud.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositoryimpl usrepo = new UsuarioRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        private readonly UnitOfWork useruni = new UnitOfWork(new BbtEstablecimientosDeSaludContext());
        // GET: Usuario/Perfil
        public IActionResult Perfil()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        // POST: Usuario/Registrar
        [HttpPost]
        public IActionResult Registrar(Usuario objUsu)
        {
            if (ModelState.IsValid)
            {
                usrepo.Registrar(objUsu);
                useruni.SaveChanges();
                return View(objUsu);
            }
            else
            {
                return View(objUsu);
            }
        }
        // GET: Usuario/Perfil?idUs=1
        [HttpGet]
        public IActionResult Perfil(int idUs)
        {
            var resultado = usrepo.BuscarId(idUs);
            return View(resultado);
        }

        // POST: Usuario/Perfil
        [HttpPost]
        public IActionResult Perfil(Usuario objUsu)
        {
            usrepo.Registrar(objUsu);
            useruni.SaveChanges();
            return Redirect("~/Usuario/Perfil?idUs=" + objUsu.Id);
        }
    }
}
