using Moq;
using Moq.AutoMock;
using Simulado.Application;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Simulado.Application.MapperConfig;
using System.Collections.Generic;

namespace Simulado.Teste.Application
{
    public class DisciplinaApplicationTest
    {
        private readonly AutoMocker _mocker;
        private readonly DisciplinaApplication _appServiceDisciplina;

        public DisciplinaApplicationTest()
        {
            _mocker = new AutoMocker();
            _appServiceDisciplina = _mocker.CreateInstance<DisciplinaApplication>();
        }

        [Fact(DisplayName = "Adicionar disciplina com campos validos")]
        public async Task Disciplina_CamposValido_DeveRetornarSucesso()
        {
            //Arrange
            var disciplinaVm = new DisciplinaViewModel() { Descricao = "História" };

            var respositoryMock = new Mock<IDisciplinaRepository>();

            respositoryMock.Setup(r => r.UnitOfWorks.Commit())
            .Returns(Task.FromResult(true));

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });
            var mapper = mockMapper.CreateMapper();

            var logger = new Mock<ILogger<DisciplinaApplication>>();

            //Act
            var application = new DisciplinaApplication(respositoryMock.Object, mapper: mapper, logger.Object);
            var result = await application.Adicionar(disciplinaVm);

            //Assert
            respositoryMock.Verify(r => r.Adicionar(It.IsAny<Disciplina>()), Times.Once);
            respositoryMock.Verify(r => r.UnitOfWorks.Commit(), Times.Once);

            result.Message.Should().Be("Disciplina adicionada com sucesso!");
        }

        [Fact(DisplayName = "Adicionar disciplina com disciplina vazia")]
        public async Task Disciplina_DisciplinaVazia_DeveRetornarErro()
        {
            //Arrange
            var disciplinaVm = new DisciplinaViewModel() { Descricao = "" };

            var respositoryMock = new Mock<IDisciplinaRepository>();

            respositoryMock.Setup(r => r.UnitOfWorks.Commit())
            .Returns(Task.FromResult(true));

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });
            var mapper = mockMapper.CreateMapper();

            var logger = new Mock<ILogger<DisciplinaApplication>>();

            //Act
            var application = new DisciplinaApplication(respositoryMock.Object, mapper: mapper, logger.Object);
            var result = await application.Adicionar(disciplinaVm);

            //Assert
            respositoryMock.Verify(r => r.Adicionar(It.IsAny<Disciplina>()), Times.Never);
            respositoryMock.Verify(r => r.UnitOfWorks.Commit(), Times.Never);

            result.Erros[0].Should().Be("Informe o nome da disciplina");
        }


        [Fact(DisplayName = "Atualizar disciplina com campos validos")]
        public async Task Disciplina_AtualizarCamposValido_DeveRetornarSucesso()
        {
            //Arrange
            var disciplinaEntity = new Disciplina("Matematica", Simulado.Domain.Enum.TipoDisciplinaEnum.SIMULADO, 10);

            var disciplinaVm = new DisciplinaViewModel() { Descricao = "História" };
            disciplinaVm.DisciplinaId = disciplinaEntity.Id;

            _mocker.GetMock<IDisciplinaRepository>().Setup(p => p.ObterPorId(disciplinaEntity.Id))
             .Returns(Task.FromResult(disciplinaEntity));

            _mocker.GetMock<IDisciplinaRepository>().Setup(r => r.UnitOfWorks.Commit())
              .Returns(Task.FromResult(true));

            //Act
            var result = await _appServiceDisciplina.Atualizar(disciplinaVm);

            //Assert
            _mocker.GetMock<IDisciplinaRepository>().Verify(r => r.Atualizar(It.IsAny<Disciplina>()), Times.Once);
            _mocker.GetMock<IDisciplinaRepository>().Verify(r => r.UnitOfWorks.Commit(), Times.Once);

            result.Message.Should().Be("Disciplina atualizada com sucesso!");
        }
    }
}
