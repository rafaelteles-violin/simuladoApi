using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Entity
{
    public class RespostaSaldo : EntityBase
    {
        public string NomeCandidato { get; set; }
        public int TotalAcerto { get; set; }
        public int TotalErro { get; set; }
        public int TotalQuestao { get; set; }

        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public Guid IgrejaId { get; set; }
        public Igreja Igreja { get; set; }
        public string Identificador { get; set; }

        public RespostaSaldo() { }

        public RespostaSaldo(string nomeCandidato, int acerto, int erro, 
            Guid disciplinaId, Guid igrejaId, string identificador)
        {
            NomeCandidato = nomeCandidato;
            TotalAcerto = acerto;
            TotalErro = erro;
            DisciplinaId = disciplinaId;
            IgrejaId = igrejaId;
            Identificador = identificador;
        }


        public override ValidationResult ValidarEntidade()
        {
            throw new NotImplementedException();
        }
    }
}
