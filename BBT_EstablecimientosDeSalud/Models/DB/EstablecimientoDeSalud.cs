using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BBT_EstablecimientosDeSalud.Models.DB;

public partial class EstablecimientoDeSalud
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public decimal Longitud { get; set; }

    public decimal Latitud { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<EpsEstablecimientoDeSalud> EpsEstablecimientoDeSaluds { get; set; } = new List<EpsEstablecimientoDeSalud>();

    public List<EstablecimientoDeSalud> Buscar(string criterio, int epsid)
    {
        List<EstablecimientoDeSalud> ListEst = new List<EstablecimientoDeSalud>();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var EstSalud = from datos in db.EstablecimientoDeSaluds
                               join epsEst in db.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsid &&
                                     (datos.Nombre.ToLower().Contains(criterio.ToLower()) || datos.Descripcion.ToLower().Contains(criterio.ToLower()))
                               select datos;

                ListEst = EstSalud.ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return ListEst;
    }
    public EstablecimientoDeSalud BuscarId(int EstId)
    {
        EstablecimientoDeSalud objEst = new EstablecimientoDeSalud();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                objEst = db.EstablecimientoDeSaluds
                .Include(e => e.EpsEstablecimientoDeSaluds)
                .FirstOrDefault(e => e.Id == EstId);
                //objEst = db.EstablecimientoDeSaluds
                //.Include(e => e.EpsEstablecimientoDeSaluds)
                //.FirstOrDefault(e => e.Id == EstId);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEst;
    }
    public List<EstablecimientoDeSalud> Listar(int epsid)
    {
        List<EstablecimientoDeSalud> objEst = new List<EstablecimientoDeSalud>();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var EstSalud = from datos in db.EstablecimientoDeSaluds
                               join epsEst in db.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsid
                               select datos;

                objEst = EstSalud.ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEst;
    }
    public List<EstablecimientoDeSalud> ListarMap()
    {
        List<EstablecimientoDeSalud> objEst = new List<EstablecimientoDeSalud>();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var EstSalud = from datos in db.EstablecimientoDeSaluds select datos;
                objEst = EstSalud.ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEst;
    }
}
