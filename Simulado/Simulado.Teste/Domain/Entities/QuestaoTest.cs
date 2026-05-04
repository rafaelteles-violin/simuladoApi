using FluentAssertions;
using Simulado.Domain.Entity;
using System;
using Xunit;

namespace Simulado.Teste.Domain.Entities
{
    public class QuestaoTest
    {
        [Fact(DisplayName = "Adicionar questão com campos validos")]
        public void Questao_CamposValido_DeveRetornarSucesso()
        {
            //Arrange
            var descricao = "Quanto é 1+1?";
            var disciplinaId = Guid.NewGuid();

            var questao = new Questao(disciplinaId, descricao);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Adicionar questão sem diciplina")]
        public void Questao_SemDisciplina_DeveRetornarErro()
        {
            //Arrange
            var descricao = "Quanto é 1+1?";
            var disciplinaId = new Guid();

            var questao = new Questao(disciplinaId, descricao);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeNull("Informe a disciplina");
        }

        [Fact(DisplayName = "Adicionar questão sem descrição")]
        public void Questao_SemDescricao_DeveRetornarErro()
        {
            //Arrange
            var descricao = "";
            var disciplinaId = Guid.NewGuid();

            var questao = new Questao(disciplinaId, descricao);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeNull("Preencha a questão");
        }

        [Fact(DisplayName = "Adicionar questão campos vazios")]
        public void Questao_CamposVazio_DeveRetornarErro()
        {
            //Arrange
            var descricao = "";
            var disciplinaId = new Guid();

            var questao = new Questao(disciplinaId, descricao);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeNull("Preencha a questão");
            result.Errors.Should().NotBeNull("Informe a disciplina");
        }

        [Fact(DisplayName = "Atualizar questão campos Validos")]
        public void Questao_AtualizarCamposVazio_DeveRetornarSucesso()
        {
            //Arrange
            var descricao = "Quanto é 1 + 1"; 
            var disciplinaId = Guid.NewGuid();

            var novaDescricao = "Quanto é 10+10";

            var questao = new Questao(disciplinaId, descricao);

            var disciplina = new Disciplina("Geografia", Simulado.Domain.Enum.TipoDisciplinaEnum.SIMULADO, 10);

            questao.AtualizarQuestao(novaDescricao, disciplina);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeTrue();
            questao.Descricao.Should().Be(novaDescricao);
        }
    }
}
