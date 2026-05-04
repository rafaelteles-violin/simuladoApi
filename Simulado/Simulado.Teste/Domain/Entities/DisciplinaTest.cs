using FluentAssertions;
using Simulado.Domain.Entity;
using Xunit;

namespace Simulado.Teste.Domain.Entities
{
    public class DisciplinaTest
    {
        [Fact(DisplayName = "Adicionar disciplina com campos validos")]
        public void Questao_CamposValido_DeveRetornarSucesso()
        {
            //Arrange
            var disciplina = "Informática";
            var questao = new Disciplina(disciplina, Simulado.Domain.Enum.TipoDisciplinaEnum.AVALIACAO, 2);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Adicionar disciplina com campo vazio")]
        public void Disciplina_CamposVazio_DeveRetornarErro()
        {
            //Arrange
            var disciplina = "";
            var questao = new Disciplina(disciplina, Simulado.Domain.Enum.TipoDisciplinaEnum.SIMULADO, 3);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeNull("Informe o nome da disciplina");
        }

        [Fact(DisplayName = "Atualizar disciplina com campos validos")]
        public void AtualizarDisciplina_CamposValido_DeveRetornarSucesso()
        {
            //Arrange
            var disciplina = "Informática";
            var novaDisciplina = "História";
            var disciplinaEntity = new Disciplina(disciplina, Simulado.Domain.Enum.TipoDisciplinaEnum.SIMULADO, 3);

            //Act
            disciplinaEntity.Atualizar(novaDisciplina,Simulado.Domain.Enum.TipoDisciplinaEnum.AVALIACAO, 2);
            var result = disciplinaEntity.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeTrue();
            disciplinaEntity.Descricao.Should().Be(novaDisciplina);

        }

        [Fact(DisplayName = "Adicionar disciplina com mais de 150 caracteres")]
        public void Disciplina_AcimaDe150Caracteres_DeveRetornarErro()
        {
            //Arrange
            var disciplina = $"Geografia do Brasil Geografia do Brasil Geografia do Brasil Geografia do Brasil" +
                $" Geografia do Brasil" +
                $" Geografia do Brasil " +
                $"Geografia do Brasil" +
                $" Geografia do Brasil" +
                $" Geografia do Brasil";

            var questao = new Disciplina(disciplina, Simulado.Domain.Enum.TipoDisciplinaEnum.SIMULADO, 3);

            //Act
            var result = questao.ValidarEntidade();

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeNull("Não é possível adicionar disiplina acima de 150 caracteres");
        }
    }
}
