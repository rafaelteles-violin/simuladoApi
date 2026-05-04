using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Entity.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Lixeira { get; set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            DataCadastro = HorarioBrasilia.Get();
            DataAlteracao = HorarioBrasilia.Get();
        }

        public void EnviarParaLixeira() => Lixeira = true;

        public abstract ValidationResult ValidarEntidade();
    }
}
