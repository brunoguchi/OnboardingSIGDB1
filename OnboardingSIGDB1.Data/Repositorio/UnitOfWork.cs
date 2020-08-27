using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SIGDB1Context _context;

        public UnitOfWork(SIGDB1Context context)
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

        public async Task<bool> Commit()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Rollback() { }
    }
}
