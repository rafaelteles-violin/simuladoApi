using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Service
{
    public interface IServiceResposta
    {
        Task RemoverAlternativaResposta(List<Guid> alternativasId);
        Task AdicionarAlternativaResposta(AlternativaResposta alternativa);
    }
}
