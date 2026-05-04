using FluentValidation.Results;
using Simulado.Domain.Entity.Base;
using System;

namespace Simulado.Domain.Entity
{
    public class AlternativaResposta : EntityBase
    {
        public Guid RespostaId { get; private set; }
        public Resposta Resposta { get; private set; }
        public string Descricao { get; private set; }
        public int Posicao { get; private set; }
        public bool Correta { get; private set; }
        public bool Selecionada { get; private set; }

        private AlternativaResposta() { }

        public AlternativaResposta(Guid respostaId, string descricao, int posicao, bool correta, bool selecionada)
        {
            RespostaId = respostaId;
            Descricao = descricao;
            Posicao = posicao;
            Correta = correta;
            Selecionada = selecionada;
        }

        public override ValidationResult ValidarEntidade()
        {
            throw new NotImplementedException();
        }
    }
}
