using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IDisciplinaApplication
    {
        Task<ServerStatus> Adicionar(DisciplinaViewModel disciplinaVm);
        Task<ServerStatus> Atualizar(DisciplinaViewModel disciplinaVm);
        Task<DisciplinaViewModel> ObterDisciplinaPorId(Guid disciplinaId);
        Task<List<DisciplinaViewModel>> ObterTodasDisciplinas();

        Task<ServerStatus> RemoverDisciplina(Guid id);
    }
}
