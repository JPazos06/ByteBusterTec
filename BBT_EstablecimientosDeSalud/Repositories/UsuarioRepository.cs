using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de usuarios.
    /// </summary>
    public interface UsuarioRepository
    {
        /// <summary>
        /// Registra un usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto Usuario que se registrará.</param>
        void Registrar(Usuario usuario);

        /// <summary>
        /// Realiza el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="usuarioCuenta">Nombre de usuario o correo electrónico.</param>
        /// <param name="contrasenaCuenta">Contraseña del usuario.</param>
        /// <returns>El objeto Usuario correspondiente al inicio de sesión.</returns>
        Usuario Login(string usuarioCuenta, string contrasenaCuenta);

        /// <summary>
        /// Busca un usuario por su ID.
        /// </summary>
        /// <param name="usuarioId">ID del usuario a buscar.</param>
        /// <returns>El objeto Usuario correspondiente al ID proporcionado.</returns>
        Usuario BuscarId(int usuarioId);
    }

    /// <summary>
    /// Implementación del repositorio de usuarios.
    /// </summary>
    public class UsuarioRepositoryimpl : UsuarioRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public UsuarioRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void Registrar(Usuario usuario)
        {
            try
            {
                if (usuario.Id > 0)
                {
                    _dbContext.Entry(usuario).State = EntityState.Modified;
                }
                else
                {
                    _dbContext.Entry(usuario).State = EntityState.Added;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public Usuario Login(string usuarioCuenta, string contrasenaCuenta)
        {
            Usuario usuario = new Usuario();
            try
            {
                var usuarioDatos = from datos in _dbContext.Usuarios select datos;
                usuario = usuarioDatos.Where(u => u.Email == usuarioCuenta && u.Contrasena == contrasenaCuenta).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuario;
        }

        /// <inheritdoc />
        public Usuario BuscarId(int usuarioId)
        {
            Usuario usuario = new Usuario();
            try
            {
                var usuarioDatos = from datos in _dbContext.Usuarios select datos;
                usuario = usuarioDatos.Where(e => e.Id == usuarioId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuario;
        }
    }

    /// <summary>
    /// Define la interfaz para la unidad de trabajo.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        UsuarioRepository UsuarioRepositoryimpl { get; }
        void SaveChanges();
    }

    /// <summary>
    /// Implementación de la unidad de trabajo.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;
        private UsuarioRepository _usuarioRepository;

        /// <summary>
        /// Constructor de la clase UnitOfWork.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public UnitOfWork(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public UsuarioRepository UsuarioRepositoryimpl
        {
            get
            {
                if (_usuarioRepository == null)
                {
                    _usuarioRepository = new UsuarioRepositoryimpl(_dbContext);
                }
                return _usuarioRepository;
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
