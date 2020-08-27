using OnboardingSIGDB1.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioSIGDB1 : IRepositorioSIGDB1
    {
        private readonly SIGDB1Context _context;

        public RepositorioSIGDB1(SIGDB1Context context)
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
