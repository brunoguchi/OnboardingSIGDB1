using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Base.Entidades;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private readonly SIGDB1Context _context;

        public RepositorioBase(SIGDB1Context context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }
    }
}
