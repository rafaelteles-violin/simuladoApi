using FluentValidation;
using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using Simulado.Domain.Enum;
using System.Collections.Generic;

namespace Simulado.Domain.Entity
{
    public class Disciplina : EntityBase
    {
        public string Descricao { get; private set; }
        public TipoDisciplinaEnum TipoDisciplina { get; set; }
        public int TotalExibicaoQuestao { get; private set; }

        public List<Questao> _questoes;
        public IReadOnlyCollection<Questao> Questoes => _questoes;

        public List<RespostaSaldo> RespostaSaldo { get; set; }

        private Disciplina() { }

        public Disciplina(string descricao, TipoDisciplinaEnum tipoDisciplina,
            int totalExibicaoQuestao)
        {
            Descricao = descricao;
            TipoDisciplina = tipoDisciplina;
            TotalExibicaoQuestao = totalExibicaoQuestao;
        }

        public void Atualizar(string descricao, TipoDisciplinaEnum tipoDisciplina,
             int totalExibicaoQuestao)
        {
            Descricao = descricao;
            TipoDisciplina = tipoDisciplina;
            TotalExibicaoQuestao = totalExibicaoQuestao;
        }


        public class DisciplinaValidator : AbstractValidator<Disciplina>
        {
            public DisciplinaValidator()
            {
                RuleFor(x => x.Descricao)
                    .NotNull()
                    .NotEmpty()                    
                    .WithMessage("Informe o nome da disciplina")
                    .MaximumLength(150)
                    .WithMessage("Não é possível adicionar disiplina acima de 150 caracteres");
            }
        }

        public override ValidationResult ValidarEntidade()
        {
            return new DisciplinaValidator().Validate(this);
        }
    }
}
