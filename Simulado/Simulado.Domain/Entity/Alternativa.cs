using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Entity
{
    public class Alternativa : EntityBase
    {
        public Guid QuestaoId { get; private set; }
        public Questao Questao { get; private set; }
        public string Descricao { get; private set; }
        public int Posicao { get; private set; }
        public bool Correta { get; private set; }

        private Alternativa() { }

        public Alternativa(Guid questaoId, string descricao, int posicao, bool correta)
        {
            QuestaoId = questaoId;
            Descricao = descricao;
            Posicao = posicao;
            Correta = correta;
        }

        public override ValidationResult ValidarEntidade()
        {
            throw new NotImplementedException();
        }
    }
}
