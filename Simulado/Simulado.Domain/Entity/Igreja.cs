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
    public class Igreja : EntityBase
    {

        private Igreja() { }
        public Igreja(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
        public List<RespostaSaldo> RespostaSaldo { get; set; }
        public List<UsuarioIgreja> UsuarioIgreja { get; set; }


        public override ValidationResult ValidarEntidade()
        {
            return new IgrejaValidator().Validate(this);
        }

        public class IgrejaValidator : AbstractValidator<Igreja>
        {
            public IgrejaValidator()
            {
                RuleFor(x => x.Nome)
               .NotNull()
               .NotEmpty()
               .WithMessage("Preencha o campo nome")
               .MaximumLength(200)
               .WithMessage("Não é permitido questões acima de 200 caracteres");
            }
        }
    }
}
