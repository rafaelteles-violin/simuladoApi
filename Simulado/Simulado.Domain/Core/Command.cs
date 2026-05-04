using FluentValidation.Results;
using MediatR;
using System;

namespace Simulado.Domain.Core
{
    public abstract class Command : IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool EstaValido()
        {
            return ValidationResult.IsValid;
        }

        public void AdicionarErrosDeProcessamentoDoComando(string mensagemDeErro)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagemDeErro));
        }
    }
}
