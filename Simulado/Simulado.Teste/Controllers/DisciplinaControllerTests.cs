using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Moq;
using Simulado.API.Controllers;
using Simulado.Application.Interface;
using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Simulado.Teste.Controllers
{
    public class DisciplinaControllerTests
    {
        private readonly Mock<IDisciplinaApplication> _application = new();
        private readonly Mock<ILogger<DisciplinaController>> _logger = new();
        private readonly Mock<IMediator> _mediator = new();
        private readonly DisciplinaController _controller;

        public DisciplinaControllerTests()
        {
            _controller = new DisciplinaController(_application.Object, _logger.Object, _mediator.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarDisciplinasDaAplicacao()
        {
            var expected = new List<DisciplinaViewModel> { new() { Descricao = "Matemática" } };
            _application.Setup(x => x.ObterTodasDisciplinas()).ReturnsAsync(expected);
            var result = await _controller.ObterTodos();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task ObterPorId_DeveDelegarIdParaAplicacao()
        {
            var id = Guid.NewGuid();
            var expected = new DisciplinaViewModel { DisciplinaId = id, Descricao = "História" };
            _application.Setup(x => x.ObterDisciplinaPorId(id)).ReturnsAsync(expected);
            var result = await _controller.ObterPorId(id);
            result.Should().BeSameAs(expected);
            _application.Verify(x => x.ObterDisciplinaPorId(id), Times.Once);
        }

        [Fact]
        public async Task Remover_DeveRetornarOkQuandoStatusNaoContemErros()
        {
            _application.Setup(x => x.RemoverDisciplina(It.IsAny<Guid>())).ReturnsAsync(new ServerStatus("removida"));
            var result = await _controller.Remover(Guid.NewGuid());
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Remover_DeveDelegarIdParaAplicacao()
        {
            var id = Guid.NewGuid();
            _application.Setup(x => x.RemoverDisciplina(id)).ReturnsAsync(new ServerStatus("removida"));
            await _controller.Remover(id);
            _application.Verify(x => x.RemoverDisciplina(id), Times.Once);
        }
    }
}