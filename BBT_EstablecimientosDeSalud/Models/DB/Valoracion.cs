using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BBT_EstablecimientosDeSalud.Models.DB;

public partial class Valoracion
{
    public int Id { get; set; }

    public int EstablecimientoId { get; set; }

    public int UsuarioId { get; set; }

    public string? Comentario { get; set; }

    public int Valoracion1 { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public void Guardar()
    {
        try
        {
            using (var db = new BbtEstablecimientosDeSaludContext())
            {
                db.Entry(this).State = EntityState.Added;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public List<Valoracion> ListarId(int EstId)
    {
        List<Valoracion> objEst = new List<Valoracion>();
        try
        {
            using (var db = new Models.DB.BbtEstablecimientosDeSaludContext())
            {
                var valo = from datos in db.Valoracions select datos;
                objEst = valo.Where(e => e.EstablecimientoId == EstId).ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return objEst;
    }
}
