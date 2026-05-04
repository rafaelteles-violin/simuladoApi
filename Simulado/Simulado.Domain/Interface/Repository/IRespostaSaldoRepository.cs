using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IRespostaSaldoRepository : IRepositoryBase<RespostaSaldo>
    {
        Task<List<RespostaSaldo>> ObterRespostaSaldo();
        Task<List<RespostaSaldo>> ObterRespostaSaldoPorIgrejas(List<Guid> idsIgrejas);
        Task<List<RespostaSaldo>> ObterRespostaSaldoAvaliacao();
    }
}
