using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BBT_EstablecimientosDeSalud.Models.DB;

public partial class EpsEstablecimientoDeSalud
{
    public int Id { get; set; }

    public int EpsId { get; set; }

    public int EstablecimientoId { get; set; }

    public virtual Ep Eps { get; set; } = null!;

    public virtual EstablecimientoDeSalud Establecimiento { get; set; } = null!;
    public EpsEstablecimientoDeSalud BuscarId(int EstId)
    {
        EpsEstablecimientoDeSalud objEp = new EpsEstablecimientoDeSalud();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {

                var EstSalud = from datos in db.EpsEstablecimientoDeSaluds select datos;
                objEp = EstSalud.Where(e => e.EstablecimientoId == EstId).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEp;
    }
    public Ep BuscarIdEps(int EstId)
    {
        Ep objEp = new Ep();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                objEp = db.EpsEstablecimientoDeSaluds
                    .Include(e => e.Eps)
                    .FirstOrDefault(e => e.EstablecimientoId == EstId)?.Eps;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEp;
    }
}
