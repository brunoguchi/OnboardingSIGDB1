using OnboardingSIGDB1.Domain.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Base.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
