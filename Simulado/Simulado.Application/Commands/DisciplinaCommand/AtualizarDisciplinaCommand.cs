using Simulado.Application.ViewModel;
using Simulado.Domain.Core;
using Simulado.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Commands.DisciplinaCommand
{
    public class AtualizarDisciplinaCommand : Command
    {
        public Guid DisciplinaId { get; set; }
        public string Descricao { get; set; }
        public TipoDisciplinaEnum TipoDisciplina { get; set; }
        public int TotalExibicaoQuestao { get; private set; }

        public AtualizarDisciplinaCommand(DisciplinaViewModel disciplinaVm)
        {
            DisciplinaId = disciplinaVm.DisciplinaId;
            Descricao = disciplinaVm.Descricao;
            TipoDisciplina = disciplinaVm.TipoDisciplina;
            TotalExibicaoQuestao = disciplinaVm.TotalExibicaoQuestao;
        }
    }
}
