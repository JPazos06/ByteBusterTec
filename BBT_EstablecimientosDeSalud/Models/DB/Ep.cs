using System;
using System.Collections.Generic;

namespace BBT_EstablecimientosDeSalud.Models.DB;

public partial class Ep
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<EpsEstablecimientoDeSalud> EpsEstablecimientoDeSaluds { get; set; } = new List<EpsEstablecimientoDeSalud>();

    public Ep BuscarId(int EpsId)
    {
        Ep objEps = new Ep();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var EpsID = from datos in db.Eps select datos;
                objEps = EpsID.Where(e => e.Id == EpsId).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEps;
    }
    public List<Ep> Listar()
    {
        List<Ep> objEps = new List<Ep>();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var Eps = from datos in db.Eps select datos;
                objEps = Eps.ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEps;
    }
}
