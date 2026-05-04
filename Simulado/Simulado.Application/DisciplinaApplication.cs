using AutoMapper;
using Microsoft.Extensions.Logging;
using Simulado.Application.Interface;
using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.Application
{
    public class DisciplinaApplication : IDisciplinaApplication
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DisciplinaApplication(IDisciplinaRepository disciplinaRepository,
                                     IMapper mapper,
                                     ILogger<DisciplinaApplication> logger)
        {
            _disciplinaRepository = disciplinaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServerStatus> Adicionar(DisciplinaViewModel disciplinaVm)
        {
            var disciplina = _mapper.Map<Disciplina>(disciplinaVm);

            if (!disciplina.ValidarEntidade().IsValid)
            {
                _logger.LogInformation("Falha na validação da entidade disciplina");

                return await Task.FromResult(new ServerStatus(disciplina.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            await _disciplinaRepository.Adicionar(disciplina);

            if (!await _disciplinaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar disciplina");

            _logger.LogInformation($"Disciplina {disciplina.Descricao} adicionada com sucesso");

            return await Task.FromResult(new ServerStatus("Disciplina adicionada com sucesso!"));
        }

        public async Task<ServerStatus> Atualizar(DisciplinaViewModel disciplinaVm)
        {
            var disciplina = await _disciplinaRepository.ObterPorId(disciplinaVm.DisciplinaId);

            disciplina.Atualizar(disciplinaVm.Descricao, disciplinaVm.TipoDisciplina, disciplinaVm.TotalExibicaoQuestao);

            if (!disciplina.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(disciplina.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            _disciplinaRepository.Atualizar(disciplina);

            if (!await _disciplinaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao atualizar disciplina");

            _logger.LogInformation($"Disciplina {disciplina.Descricao} atualizada com sucesso");

            return await Task.FromResult(new ServerStatus("Disciplina atualizada com sucesso!"));
        }

        public async Task<DisciplinaViewModel> ObterDisciplinaPorId(Guid disciplinaId)
        {
            var disciplina = await _disciplinaRepository.ObterPorId(disciplinaId);
            return _mapper.Map<DisciplinaViewModel>(disciplina);
        }

        public async Task<List<DisciplinaViewModel>> ObterTodasDisciplinas()
        {
            var disciplina = await _disciplinaRepository.ObterDisciplinas();
            return _mapper.Map<List<DisciplinaViewModel>>(disciplina);
        }


        public async Task<ServerStatus> RemoverDisciplina(Guid id)
        {
            var disciplina = await _disciplinaRepository.ObterPorId(id);

            await _disciplinaRepository.Remover(disciplina);

            if (!await _disciplinaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao remover disciplina");

            return await Task.FromResult(new ServerStatus($"{disciplina.Descricao} removido com sucesso!"));

        }
    }
}
