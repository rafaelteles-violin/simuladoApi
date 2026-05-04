using FluentValidation.Results;
using MediatR;
using Simulado.Domain.Core;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Simulado.Application.Commands.DisciplinaCommand
{
    public class DisciplinaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarDisciplinaCommand, ValidationResult>,
        IRequestHandler<AtualizarDisciplinaCommand, ValidationResult>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaCommandHandler(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarDisciplinaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido()) return request.ValidationResult;

            var disciplina = new Disciplina(request.Descricao, request.TipoDisciplina, request.TotalExibicaoQuestao);

            await _disciplinaRepository.Adicionar(disciplina);

            return await PersistirDados(_disciplinaRepository.UnitOfWorks);
        }

        public async Task<ValidationResult> Handle(AtualizarDisciplinaCommand request, CancellationToken cancellationToken)
        {
            var disciplina = await _disciplinaRepository.ObterPorId(request.DisciplinaId);
            disciplina.Atualizar(request.Descricao, request.TipoDisciplina, request.TotalExibicaoQuestao);

            if (!request.EstaValido()) return request.ValidationResult;

            _disciplinaRepository.Atualizar(disciplina);

            ValidationResult.ToString("Disciplina atualizada com sucesso");

            return await PersistirDados(_disciplinaRepository.UnitOfWorks);
        }
    }
}
