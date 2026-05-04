using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IUnitOfWorks
    {
        Task<bool> Commit();
    }
}
