using OnboardingSIGDB1.Domain.Base.Interfaces;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioBase : IRepositorioBase
    {
        private readonly SIGDB1Context _context;

        public RepositorioBase(SIGDB1Context context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
