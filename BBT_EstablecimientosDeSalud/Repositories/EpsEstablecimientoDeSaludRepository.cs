using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
namespace BBT_EstablecimientosDeSalud.Repositories
{
    public interface EpsEstablecimientoDeSaludRepository
    {
        EpsEstablecimientoDeSalud BuscarId(int EstId);
        Ep BuscarIdEps(int EstId);
    }
    public class EpsEstablecimientodeSaludRepositoryimpl : EpsEstablecimientoDeSaludRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public EpsEstablecimientodeSaludRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }
        public EpsEstablecimientoDeSalud BuscarId(int EstId)
        {
            EpsEstablecimientoDeSalud objEp = new EpsEstablecimientoDeSalud();
            try
            {
                var EstSalud = from datos in _dbContext.EpsEstablecimientoDeSaluds select datos;
                objEp = EstSalud.Where(e => e.EstablecimientoId == EstId).FirstOrDefault();
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
                objEp = _dbContext.EpsEstablecimientoDeSaluds
                        .Include(e => e.Eps)
                        .FirstOrDefault(e => e.EstablecimientoId == EstId)?.Eps;
            }
            catch (Exception ex)
            {
                throw;
            }
            return objEp;
        }
    }

}
