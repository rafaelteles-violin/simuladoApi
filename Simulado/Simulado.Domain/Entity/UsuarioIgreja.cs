using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Entity
{
    public class UsuarioIgreja : EntityBase
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Guid IgrejaId { get; set; }
        public Igreja Igreja { get; set; }

        public override ValidationResult ValidarEntidade()
        {
            throw new NotImplementedException();
        }
    }
}
