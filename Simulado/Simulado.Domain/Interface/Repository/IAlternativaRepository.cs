using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IAlternativaRepository : IRepositoryBase<Alternativa>
    {
        Task<List<Alternativa>> ObterAlternativasDaQuestao(Guid questaoId);
    }
}
