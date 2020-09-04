using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Base.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
