using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
namespace BBT_EstablecimientosDeSalud.Repositories
{
    public interface EpRepository
    {
        Ep BuscarId(int EpsId);
        List<Ep> Listar();
    }
    public class EpRepositoryimpl : EpRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;
        public EpRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Ep BuscarId(int EpsId)
        {
            Ep objEps = new Ep();
            try
            {
                var EpsID = from datos in _dbContext.Eps select datos;
                objEps = EpsID.Where(e => e.Id == EpsId).FirstOrDefault();
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
                var Eps = from datos in _dbContext.Eps select datos;
                objEps = Eps.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return objEps;
        }
    }
}
