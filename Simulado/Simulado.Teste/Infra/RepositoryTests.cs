using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Entity;
using Simulado.Domain.Enum;
using Simulado.Infra.Data;
using Simulado.Infra.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Simulado.Teste.Infra
{
    public class RepositoryTests
    {
        private static SimuladoContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<SimuladoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new SimuladoContext(options);
        }

        [Fact]
        public async Task RepositoryBase_AdicionarEObterPorId_DevePersistirEntidade()
        {
            using var context = CreateContext();
            var repository = new RepositoryBase<Disciplina>(context);
            var disciplina = new Disciplina("Matemática", TipoDisciplinaEnum.SIMULADO, 10);
            await repository.Adicionar(disciplina);
            await context.SaveChangesAsync();
            var result = await repository.ObterPorId(disciplina.Id);
            result.Should().NotBeNull();
            result.Id.Should().Be(disciplina.Id);
            result.Descricao.Should().Be("Matemática");
        }

        [Fact]
        public async Task DisciplinaRepository_ObterDisciplinas_DeveIgnorarLixeiraEOrdenar()
        {
            using var context = CreateContext();
            context.Disciplina.AddRange(
                new Disciplina("Zoologia", TipoDisciplinaEnum.SIMULADO, 10),
                new Disciplina("História", TipoDisciplinaEnum.SIMULADO, 10),
                new Disciplina("Excluída", TipoDisciplinaEnum.SIMULADO, 10) { Lixeira = true });
            await context.SaveChangesAsync();
            var repository = new DisciplinaRepository(context);
            var result = await repository.ObterDisciplinas();
            result.Should().HaveCount(2);
            result.Select(x => x.Descricao).Should().BeInAscendingOrder();
            result.Should().NotContain(x => x.Lixeira);
        }
    }
}