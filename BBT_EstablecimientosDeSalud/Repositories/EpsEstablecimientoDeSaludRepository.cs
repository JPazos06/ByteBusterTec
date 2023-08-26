using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de relación entre EPS y Establecimientos de Salud.
    /// </summary>
    public interface EpsEstablecimientoDeSaludRepository
    {
        /// <summary>
        /// Busca una relación entre EPS y Establecimiento de Salud por ID de establecimiento.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento de salud a buscar.</param>
        /// <returns>La relación EPS-Establecimiento correspondiente al ID proporcionado.</returns>
        EpsEstablecimientoDeSalud BuscarId(int establecimientoId);

        /// <summary>
        /// Busca una EPS asociada a un Establecimiento de Salud por ID de establecimiento.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento de salud para el que se busca la EPS asociada.</param>
        /// <returns>La EPS asociada al establecimiento de salud.</returns>
        Ep BuscarIdEps(int establecimientoId);
    }

    /// <summary>
    /// Implementación del repositorio de relación entre EPS y Establecimientos de Salud.
    /// </summary>
    public class EpsEstablecimientodeSaludRepositoryimpl : EpsEstablecimientoDeSaludRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        /// <summary>
        /// Constructor de la clase EpsEstablecimientodeSaludRepositoryimpl.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public EpsEstablecimientodeSaludRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public EpsEstablecimientoDeSalud BuscarId(int establecimientoId)
        {
            EpsEstablecimientoDeSalud epsEstablecimiento = new EpsEstablecimientoDeSalud();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EpsEstablecimientoDeSaluds select datos;
                epsEstablecimiento = establecimientoDatos.Where(e => e.EstablecimientoId == establecimientoId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return epsEstablecimiento;
        }

        /// <inheritdoc />
        public Ep BuscarIdEps(int establecimientoId)
        {
            Ep eps = new Ep();
            try
            {
                eps = _dbContext.EpsEstablecimientoDeSaluds
                        .Include(e => e.Eps)
                        .FirstOrDefault(e => e.EstablecimientoId == establecimientoId)?.Eps;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return eps;
        }
    }
}
