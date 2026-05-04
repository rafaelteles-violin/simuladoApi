using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IDisciplinaRepository : IRepositoryBase<Disciplina>
    {
        Task<List<Disciplina>> ObterQuantidadeDeQuestaoPorDisciplina();
        Task<List<Disciplina>> ObterDisciplinas();
    }
}
