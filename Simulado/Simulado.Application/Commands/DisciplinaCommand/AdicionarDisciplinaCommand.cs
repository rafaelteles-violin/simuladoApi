using FluentValidation;
using Simulado.Domain.Core;
using Simulado.Domain.Enum;


namespace Simulado.Application.Commands.DisciplinaCommand
{
    public class AdicionarDisciplinaCommand : Command
    {
        public string Descricao { get;  set; }
        public TipoDisciplinaEnum TipoDisciplina { get; set; }
        public int TotalExibicaoQuestao { get; set; }

        public AdicionarDisciplinaCommand(string descricao, TipoDisciplinaEnum tipoDisciplina, int totalExibicaoQuestao)
        {
            Descricao = descricao;
            TipoDisciplina = tipoDisciplina;
            TotalExibicaoQuestao = totalExibicaoQuestao;
        }

        public override bool EstaValido()
        {
            ValidationResult = new DisciplinaValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DisciplinaValidator : AbstractValidator<AdicionarDisciplinaCommand>
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
    }
}
