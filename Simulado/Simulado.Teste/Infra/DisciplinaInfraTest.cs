using Moq;
using Moq.AutoMock;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using Simulado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Simulado.Teste.Infra
{
    public class DisciplinaInfraTest
    {
        private readonly AutoMocker _mocker;


        [Fact(DisplayName = "Adicionar disciplina com campos validos")]
        public async void Questao_CamposValido_DeveRetornarSucesso()
        {

            var disciplinaEntity = new Disciplina("Matematica", Simulado.Domain.Enum.TipoDisciplinaEnum.AVALIACAO, 2);

            var dbContextMock = new Mock<SimuladoContext>();

            dbContextMock.Setup(r => r.AddAsync(disciplinaEntity, default));

            var disciplinaRepository = new DisciplinaRepository(dbContextMock.Object);

            await disciplinaRepository.Adicionar(disciplinaEntity);

            dbContextMock.Verify(r => r.AddAsync(It.IsAny<Disciplina>(), default), Times.Once);
        }
    }
}
