using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IQuestaoRepository : IRepositoryBase<Questao>
    {
        Task<Questao> ObterQuestaoComAlternativas(Guid questaoId);
        Task<List<Questao>> ObterTodasQuestoesComAlternativas();
        Task<List<Questao>> ObterQuestoesPorDisciplina(Guid disciplinaId);
        Task<List<Questao>> ObterQuestoesParaRealizar(Guid disciplinaId, int quantidade);
    }
}
