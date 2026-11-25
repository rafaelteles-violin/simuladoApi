using AutoMapper;
using FluentAssertions;
using Moq;
using Simulado.Application;
using Simulado.Application.MapperConfig;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Simulado.Teste.Application
{
    public class QuestaoApplicationTests
    {
        private readonly Mock<IQuestaoRepository> _questaoRepository = new();
        private readonly Mock<IDisciplinaRepository> _disciplinaRepository = new();
        private readonly Mock<IServiceQuestao> _serviceQuestao = new();
        private readonly IMapper _mapper;
        private readonly QuestaoApplication _sut;

        public QuestaoApplicationTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfig>()).CreateMapper();
            _sut = new QuestaoApplication(_questaoRepository.Object, _disciplinaRepository.Object, _mapper, _serviceQuestao.Object);
        }

        [Fact]
        public async Task Adicionar_QuestaoSemDisciplina_DeveRetornarErroSemPersistir()
        {
            var vm = new QuestaoViewModel { DisciplinaId = Guid.Empty, Descricao = "Enunciado", Alternativas = new List<AlternativaViewModel>() };
            var result = await _sut.Adicionar(vm);
            result.Erros.Should().Contain("Informe a disciplina");
            _questaoRepository.Verify(x => x.Adicionar(It.IsAny<Questao>()), Times.Never);
            _questaoRepository.Verify(x => x.UnitOfWorks.Commit(), Times.Never);
        }

        [Fact]
        public async Task Adicionar_QuestaoValida_DevePersistirEConfirmar()
        {
            var vm = new QuestaoViewModel { DisciplinaId = Guid.NewGuid(), Descricao = "Enunciado válido", Alternativas = new List<AlternativaViewModel>() };
            var unitOfWork = new Mock<IUnitOfWorks>();
            unitOfWork.Setup(x => x.Commit()).ReturnsAsync(true);
            _questaoRepository.SetupGet(x => x.UnitOfWorks).Returns(unitOfWork.Object);
            var result = await _sut.Adicionar(vm);
            result.Message.Should().Be("Questão adicionada com sucesso!");
            _questaoRepository.Verify(x => x.Adicionar(It.Is<Questao>(q => q.Descricao == vm.Descricao && q.DisciplinaId == vm.DisciplinaId)), Times.Once);
            unitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task ObterQuestoesParaRealizar_QuantidadeInvalida_DeveRetornarErro()
        {
            var result = await _sut.ObterQuestoesParaRealizar(Guid.NewGuid(), 0);
            result.Erros.Should().Contain("Informe a quantidade de questões");
            _questaoRepository.Verify(x => x.ObterQuestoesParaRealizar(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObterQuestoesParaRealizar_DisciplinaInvalida_DeveRetornarErro()
        {
            var result = await _sut.ObterQuestoesParaRealizar(Guid.Empty, 5);
            result.Erros.Should().Contain("Informe a disciplina");
            _questaoRepository.Verify(x => x.ObterQuestoesParaRealizar(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Adicionar_CommitFalha_DeveLancarExcecao()
        {
            var vm = new QuestaoViewModel { DisciplinaId = Guid.NewGuid(), Descricao = "Enunciado", Alternativas = new List<AlternativaViewModel>() };
            var unitOfWork = new Mock<IUnitOfWorks>();
            unitOfWork.Setup(x => x.Commit()).ReturnsAsync(false);
            _questaoRepository.SetupGet(x => x.UnitOfWorks).Returns(unitOfWork.Object);
            Func<Task> act = () => _sut.Adicionar(vm);
            await act.Should().ThrowAsync<Exception>().WithMessage("Erro interno ao adicionar questão");
        }
    }
}