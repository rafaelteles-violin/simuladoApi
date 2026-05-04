using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Service
{
    public interface IServiceQuestao
    {
        Task RemoverAlternativas(List<Guid> alternativasId);
        Task AdicionarAlternativa(Alternativa alternativa);
    }
}
