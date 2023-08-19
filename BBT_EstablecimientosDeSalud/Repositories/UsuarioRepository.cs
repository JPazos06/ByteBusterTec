using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Repositories
{
    public interface UsuarioRepository
    {
        void Registrar(Usuario usuario);
        Usuario Login(string user, string pass);
        Usuario BuscarId(int usId);
    }
    public class UsuarioRepositoryimpl : UsuarioRepository
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;

        public UsuarioRepositoryimpl(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public Usuario Login(string user, string pass)
        {
            Usuario usuario = new Usuario();
            try
            {
                var usuarios = from datos in _dbContext.Usuarios select datos;
                usuario = usuarios.Where(u => u.Email == user && u.Contrasena == pass).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuario;
        }

        public Usuario BuscarId(int usId)
        {
            Usuario objUs = new Usuario();
            try
            {
                var usID = from datos in _dbContext.Usuarios select datos;
                objUs = usID.Where(e => e.Id == usId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return objUs;
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        UsuarioRepository UsuarioRepositoryimpl { get; }
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BbtEstablecimientosDeSaludContext _dbContext;
        private UsuarioRepository _usuarioRepository;

        public UnitOfWork(BbtEstablecimientosDeSaludContext dbContext)
        {
            _dbContext = dbContext;
        }

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
