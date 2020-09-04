using OnboardingSIGDB1.Domain.Base.Interfaces;
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

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
