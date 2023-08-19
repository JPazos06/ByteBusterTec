using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    public interface EstablecimientoDeSaludRepository
    {
        List<EstablecimientoDeSalud> Buscar(string criterio, int epsid);
        EstablecimientoDeSalud BuscarId(int EstId);
        List<EstablecimientoDeSalud> Listar(int epsid);
        List<EstablecimientoDeSalud> ListarMap();
    }
    public class EstablecimientodeSaludRepositoryimpl : EstablecimientoDeSaludRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public EstablecimientodeSaludRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EstablecimientoDeSalud> Buscar(string criterio, int epsid)
        {
            List<EstablecimientoDeSalud> ListEst = new List<EstablecimientoDeSalud>();
            try
            {
                var EstSalud = from datos in _dbContext.EstablecimientoDeSaluds
                               join epsEst in _dbContext.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsid &&
                                     (datos.Nombre.ToLower().Contains(criterio.ToLower()) || datos.Descripcion.ToLower().Contains(criterio.ToLower()))
                               select datos;

                ListEst = EstSalud.ToList();
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
                objEst = _dbContext.EstablecimientoDeSaluds
                    .Include(e => e.EpsEstablecimientoDeSaluds)
                    .FirstOrDefault(e => e.Id == EstId);
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
                var EstSalud = from datos in _dbContext.EstablecimientoDeSaluds
                               join epsEst in _dbContext.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsid
                               select datos;

                objEst = EstSalud.ToList();
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
                var EstSalud = from datos in _dbContext.EstablecimientoDeSaluds select datos;
                objEst = EstSalud.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return objEst;
        }
    }


    public interface IUnitOfWorkEst : IDisposable
    {
        EstablecimientoDeSaludRepository EstablecimientoDeSaludRepository { get; }
        void SaveChanges();
    }

    public class UnitOfWorkEst : IUnitOfWorkEst
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;
        private EstablecimientoDeSaludRepository _establecimientodesaludrepository;

        public UnitOfWorkEst(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EstablecimientoDeSaludRepository EstablecimientoDeSaludRepository
        {
            get
            {
                if (_establecimientodesaludrepository == null)
                {
                    _establecimientodesaludrepository = new EstablecimientodeSaludRepositoryimpl(_dbContext);
                }
                return _establecimientodesaludrepository;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
