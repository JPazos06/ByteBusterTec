using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    public interface BusquedaRepository
    {
        void Registrar(Busquedum busqueda);
        List<Busquedum> ListarBusqueda();
    }
    public class BusquedaRepositoryimpl : BusquedaRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public BusquedaRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Registrar(Busquedum busqueda)
        {
            try
            {
                _dbContext.Entry(busqueda).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Busquedum> ListarBusqueda()
        {
            List<Busquedum> objBus = new List<Busquedum>();
            try
            {
                var Bus = from datos in _dbContext.Busqueda select datos;
                objBus = Bus.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return objBus;
        }
    }
}
