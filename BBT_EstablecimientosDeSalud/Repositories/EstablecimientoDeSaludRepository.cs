using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de Establecimientos de Salud.
    /// </summary>
    public interface EstablecimientoDeSaludRepository
    {
        List<EstablecimientoDeSalud> Buscar(string criterio, int epsId);
        EstablecimientoDeSalud BuscarId(int establecimientoId);
        List<EstablecimientoDeSalud> Listar(int epsId);
        List<EstablecimientoDeSalud> ListarMap();
    }

    /// <summary>
    /// Implementación del repositorio de Establecimientos de Salud.
    /// </summary>
    public class EstablecimientodeSaludRepositoryimpl : EstablecimientoDeSaludRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public EstablecimientodeSaludRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public List<EstablecimientoDeSalud> Buscar(string criterio, int epsId)
        {
            List<EstablecimientoDeSalud> listEstablecimiento = new List<EstablecimientoDeSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoDeSaluds
                               join epsEst in _dbContext.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsId &&
                                     (datos.Nombre.ToLower().Contains(criterio.ToLower()) || datos.Descripcion.ToLower().Contains(criterio.ToLower()))
                               select datos;

                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }

        /// <inheritdoc />
        public EstablecimientoDeSalud BuscarId(int estId)
        {
            EstablecimientoDeSalud establecimientoDeSalud = new EstablecimientoDeSalud();
            try
            {
                establecimientoDeSalud = _dbContext.EstablecimientoDeSaluds
                    .Include(e => e.EpsEstablecimientoDeSaluds)
                    .FirstOrDefault(e => e.Id == estId);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }

            return establecimientoDeSalud;
        }

        /// <inheritdoc />
        public List<EstablecimientoDeSalud> Listar(int epsId)
        {
            List<EstablecimientoDeSalud> listEstablecimiento = new List<EstablecimientoDeSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoDeSaluds
                               join epsEst in _dbContext.EpsEstablecimientoDeSaluds on datos.Id equals epsEst.EstablecimientoId
                               where epsEst.EpsId == epsId
                               select datos;

                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }

        /// <inheritdoc />
        public List<EstablecimientoDeSalud> ListarMap()
        {
            List<EstablecimientoDeSalud> listEstablecimiento = new List<EstablecimientoDeSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoDeSaluds select datos;
                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }
    }

    /// <summary>
    /// Define la interfaz para la unidad de trabajo de Establecimientos de Salud.
    /// </summary>
    public interface IUnitOfWorkEst : IDisposable
    {
        EstablecimientoDeSaludRepository EstablecimientoDeSaludRepository { get; }
        void SaveChanges();
    }

    /// <summary>
    /// Implementación de la unidad de trabajo de Establecimientos de Salud.
    /// </summary>
    public class UnitOfWorkEst : IUnitOfWorkEst
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;
        private EstablecimientoDeSaludRepository _establecimientodesaludrepository;

        /// <summary>
        /// Constructor de la clase UnitOfWorkEst.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public UnitOfWorkEst(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
