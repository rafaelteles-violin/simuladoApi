using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    public class QuestaoControllerTests
    {
        private readonly Mock<IQuestaoApplication> _application = new();
        private readonly QuestaoController _controller;

        public QuestaoControllerTests() { _controller = new QuestaoController(_application.Object); }

        [Fact]
        public async Task Adicionar_QuandoAplicacaoRetornaSucesso_DeveRetornarOk()
        {
            var vm = new QuestaoViewModel { Descricao = "Questão de teste" };
            _application.Setup(x => x.Adicionar(vm)).ReturnsAsync(new ServerStatus("Questão adicionada com sucesso!"));
            var result = await _controller.Adicionar(vm);
            result.Should().BeOfType<OkObjectResult>();
            _application.Verify(x => x.Adicionar(vm), Times.Once);
        }

        [Fact]
        public async Task Adicionar_QuandoAplicacaoLancaExcecao_DeveRetornarBadRequest()
        {
            var vm = new QuestaoViewModel();
            _application.Setup(x => x.Adicionar(vm)).ThrowsAsync(new Exception("erro de teste"));
            var result = await _controller.Adicionar(vm);
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("erro de teste");
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDaAplicacao()
        {
            var expected = new List<QuestaoViewModel> { new() { Descricao = "Questão 1" }, new() { Descricao = "Questão 2" } };
            _application.Setup(x => x.ObterTodasQuestoes()).ReturnsAsync(expected);
            var result = await _controller.ObterTodos();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task ObterPorId_DeveDelegarIdParaAplicacao()
        {
            var id = Guid.NewGuid();
            var expected = new QuestaoViewModel { QuestaoId = id };
            _application.Setup(x => x.ObterQuestaoPorId(id)).ReturnsAsync(expected);
            var result = await _controller.ObterPorId(id);
            result.Should().BeSameAs(expected);
            _application.Verify(x => x.ObterQuestaoPorId(id), Times.Once);
        }
    }
}