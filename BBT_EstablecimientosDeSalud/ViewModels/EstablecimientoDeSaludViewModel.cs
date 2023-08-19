using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BBT_EstablecimientosDeSalud.Models;
using BBT_EstablecimientosDeSalud.Models.DB;

namespace BBT_EstablecimientosDeSalud.ViewModels
{
    public class EstablecimientoDeSaludViewModel
    {
        public EstablecimientoDeSalud estSalud { get; set; }
        public Valoracion valoracion { get; set; }
        public List<Valoracion> listValoracion { get; set; }
        public int TotalValoraciones { get; set; }
        public Ep eps { get; set; }
        public List<EstablecimientoDeSalud> listEst { get; set; }
        public List<Ep> listEps { get; set; }
        public string Clasificacion { get; set; }
        public List<EstablecimientoDeSalud> RecEst { get; set; }
    }
}
