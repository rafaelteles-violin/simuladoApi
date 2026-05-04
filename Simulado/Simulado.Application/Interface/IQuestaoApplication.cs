using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IQuestaoApplication
    {
        Task<ServerStatus> Adicionar(QuestaoViewModel disciplinaVm);
        Task<ServerStatus> Atualizar(QuestaoViewModel disciplinaVm);
        Task<QuestaoViewModel> ObterQuestaoPorId(Guid disciplinaId);
        Task<List<QuestaoViewModel>> ObterTodasQuestoes();
        Task<List<DisciplinaQuestaoViewModel>> ObterTotalQuestaoPorDisciplina();
        Task<List<QuestaoViewModel>> ObterQuestoesPorDisciplina(Guid disciplinaId);
        Task<ServerStatus> RemoverQuestao(Guid questaoId);
        Task<ServerStatus> ObterQuestoesParaRealizar(Guid disciplinaId, int quantidade);
    }
}
