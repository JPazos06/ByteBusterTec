using Microsoft.AspNetCore.Mvc;
using BBT_EstablecimientosDeSalud.Models.DB;
using BBT_EstablecimientosDeSalud.ViewModels;
using BBT_EstablecimientosDeSalud.Models;
using BBT_EstablecimientosDeSalud.Repositories;

namespace BBT_EstablecimientosDeSalud.Controllers
{
    public class EstablecimientoDeSaludController : Controller
    {
        private readonly EstablecimientodeSaludRepositoryimpl estrepo = new EstablecimientodeSaludRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        private readonly BusquedaRepositoryimpl busqrepo = new BusquedaRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        private readonly EpRepositoryimpl eprepo = new EpRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        private readonly EpsEstablecimientodeSaludRepositoryimpl epsestrepo = new EpsEstablecimientodeSaludRepositoryimpl(new BbtEstablecimientosDeSaludContext());
        public IActionResult Buscar(string criterio, int epsid)
        {
            Busquedum objBusc = new Busquedum();
            List<EstablecimientoDeSaludViewModel> listEstvm = new List<EstablecimientoDeSaludViewModel>();
            var listEst = new List<EstablecimientoDeSalud>();
            var listEpsEst = new List<EpsEstablecimientoDeSalud>();
            if (criterio == "" || criterio == null)
            {
                listEst = estrepo.Listar(epsid).ToList();
            }
            else
            {
                listEst = estrepo.Buscar(criterio, epsid).ToList();
            }
            foreach (var item in listEst)
            {
                EstablecimientoResponse objEstResp = new EstablecimientoResponse();
                EstablecimientoDeSaludViewModel objEstvm = new EstablecimientoDeSaludViewModel();
                objEstvm.estSalud = item;
                objEstvm.eps = eprepo.BuscarId(epsestrepo.BuscarId(item.Id).EpsId);
                objEstvm.Clasificacion = objEstResp.ObtenerEstablecimiento(item.Id).Result.clasfreal;
                listEstvm.Add(objEstvm);
            }
            objBusc.TerminoBusqueda = eprepo.BuscarId(epsid).Nombre + " " + criterio;
            objBusc.UsuarioId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioId") ?? "1");
            objBusc.Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            busqrepo.Registrar(objBusc);
            return View(listEstvm);
        }
        public IActionResult Detalle(int EstId)
        {
            EstablecimientoDeSaludViewModel objEstvm = new EstablecimientoDeSaludViewModel();
            Valoracion objVal = new Valoracion();
            var IdEst = estrepo.BuscarId(EstId);
            objEstvm.estSalud = IdEst;
            objEstvm.eps = epsestrepo.BuscarIdEps(EstId);
            objEstvm.listValoracion = objVal.ListarId(IdEst.Id);
            objEstvm.TotalValoraciones = (objEstvm.listValoracion.Count() == 0) ? 0 : Convert.ToInt32(objEstvm.listValoracion.Sum(x => x.Valoracion1) / objEstvm.listValoracion.Count());
            return View(objEstvm);
        }
        [HttpPost]
        public IActionResult Valorar(Valoracion objVal)
        {
            objVal.Guardar();
            return RedirectToAction("Detalle", new { EstId = objVal.EstablecimientoId }); ;
        }
    }
}
