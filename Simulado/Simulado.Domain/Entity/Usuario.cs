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
    public class Usuario : EntityBase
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; set; }

        public List<UsuarioIgreja> UsuarioIgreja { get; set; }

        public ICollection<Resposta> Respostas { get; private set; }

        private Usuario() { }

        public Usuario(string nome, string email, string telefone, Guid usuarioId)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Id = usuarioId;            
        }

        public void Atualizar(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }


        public class UsuarioValidator : AbstractValidator<Usuario>
        {
            public UsuarioValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Informe o nome");
            }
        }

        public override ValidationResult ValidarEntidade()
        {
            return new UsuarioValidator().Validate(this);
        }


        public void AdicionarIgrejasAoUsuario(List<Guid> igrejas)
        {
            UsuarioIgreja = new List<UsuarioIgreja>();

            foreach (var igreja in igrejas)
            {
                var usuarioIgreja = new UsuarioIgreja()
                {
                    IgrejaId = igreja,
                    UsuarioId = Id
                };

                UsuarioIgreja.Add(usuarioIgreja);
            }           
        }
    }
}
