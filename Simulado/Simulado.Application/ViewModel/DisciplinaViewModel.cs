using Simulado.Domain.Entity;
using Simulado.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.ViewModel
{
    public class DisciplinaViewModel
    {
        public Guid DisciplinaId { get; set; }
        public string Descricao { get; set; }
        public TipoDisciplinaEnum TipoDisciplina { get; set; }
        public int TotalExibicaoQuestao { get;  set; }
    }

    public class DisciplinaQuestaoViewModel
    {
        public Guid DisciplinaId { get; set; }
        public string Disciplina { get; set; }
        public int Quantidade { get; set; }
        public string TipoDisciplina { get; set; }
    }
}
