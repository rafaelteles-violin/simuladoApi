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
    public class Resposta : EntityBase
    {
        public Guid QuestaoId { get; private set; }
        public Questao Questao { get; private set; }
        public string Aluno { get; private set; }
        public string Identificador { get; set; }

        public List<AlternativaResposta> _respostaAlternativas;
        public IReadOnlyCollection<AlternativaResposta> RespostaAlternativas => _respostaAlternativas;

        private Resposta() { }

        public Resposta(Guid questaoId, string aluno, string identificador)
        {
            QuestaoId = questaoId;
            Aluno = aluno;
            Identificador = identificador;
            _respostaAlternativas = new List<AlternativaResposta>();
        }

        public void AdicionarAlternativaResposta(AlternativaResposta resposta)
        {
            _respostaAlternativas.Add(resposta);
        }

        public class RespostaValidator : AbstractValidator<Resposta>
        {
            public RespostaValidator()
            {
                RuleFor(x => x.Aluno)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Aluno não pode ser nulo");

                RuleFor(x => x.QuestaoId)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage("Questão não pode ser nulo");
            }
        }

        public override ValidationResult ValidarEntidade()
        {
            return new RespostaValidator().Validate(this);
        }
    }
}
