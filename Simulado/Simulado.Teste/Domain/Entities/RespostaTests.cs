using FluentAssertions;
using Simulado.Domain.Entity;
using System;
using Xunit;

namespace Simulado.Teste.Domain.Entities
{
    public class RespostaTests
    {
        [Fact]
        public void ValidarEntidade_RespostaValida_DeveSerValida()
        {
            var resposta = new Resposta(Guid.NewGuid(), "Aluno Teste", "identificador");
            resposta.ValidarEntidade().IsValid.Should().BeTrue();
        }

        [Fact]
        public void ValidarEntidade_AlunoVazio_DeveRetornarErro()
        {
            var resposta = new Resposta(Guid.NewGuid(), "", "identificador");
            var result = resposta.ValidarEntidade();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Aluno não pode ser nulo");
        }

        [Fact]
        public void ValidarEntidade_QuestaoIdVazio_DeveRetornarErro()
        {
            var resposta = new Resposta(Guid.Empty, "Aluno Teste", "identificador");
            var result = resposta.ValidarEntidade();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Questão não pode ser nulo");
        }

        [Fact]
        public void AdicionarAlternativaResposta_DeveAdicionarNaColecao()
        {
            var resposta = new Resposta(Guid.NewGuid(), "Aluno Teste", "id");
            var alternativa = new AlternativaResposta(resposta.Id, "A", 1, true, true);
            resposta.AdicionarAlternativaResposta(alternativa);
            resposta.RespostaAlternativas.Should().ContainSingle().Which.Should().BeSameAs(alternativa);
        }
    }
}