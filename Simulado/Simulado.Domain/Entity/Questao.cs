using FluentValidation;
using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Entity
{
    public class Questao : EntityBase
    {
        public Guid DisciplinaId { get; private set; }
        public Disciplina Disciplina { get; private set; }
        public string Descricao { get; private set; }

        public List<Alternativa> Alternativas { get; set; }

        public List<Resposta> Respostas { get; set; }

        private Questao() { }

        public Questao(Guid disciplinaId, string descricao)
        {
            DisciplinaId = disciplinaId;
            Descricao = descricao;
            Alternativas = new List<Alternativa>();
        }

        public void AdicionarAlternativa(Alternativa alternativa)
        {
            Alternativas.Add(alternativa);
        }

        public void AtualizarQuestao(string descricao, Disciplina disciplina)
        {
            Descricao = descricao;
            Disciplina = disciplina;
        }

        public class QuestaoValidator : AbstractValidator<Questao>
        {
            public QuestaoValidator()
            {
                RuleFor(x => x.Descricao)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Preencha a questão")
                    .MaximumLength(800)
                    .WithMessage("Não é permitido questões acima de 800 caracteres");

                RuleFor(x => x.DisciplinaId)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Informe a disciplina");
            }
        }

        public override ValidationResult ValidarEntidade()
        {
            return new QuestaoValidator().Validate(this);
        }
    }
}
