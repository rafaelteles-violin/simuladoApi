using AutoMapper;
using Simulado.Application.Interface;
using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application
{
    public class QuestaoApplication : IQuestaoApplication
    {
        private readonly IQuestaoRepository _questaoRepository;
        private readonly IMapper _mapper;
        private readonly IServiceQuestao _serviceQuestao;
        private readonly IDisciplinaRepository _disciplinaRepository;

        public QuestaoApplication(IQuestaoRepository questaoRepository,
                                  IDisciplinaRepository disciplinaRepository,
                                  IMapper mapper,
                                  IServiceQuestao serviceQuestao)
        {
            _questaoRepository = questaoRepository;
            _mapper = mapper;
            _serviceQuestao = serviceQuestao;
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<ServerStatus> Adicionar(QuestaoViewModel questaoVm)
        {
            var questao = _mapper.Map<Questao>(questaoVm);

            if (!questao.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(questao.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            foreach (var alternativaVm in questaoVm.Alternativas)
            {
                var alternativa = new Alternativa(questao.Id, alternativaVm.Descricao,
                    alternativaVm.Posicao, alternativaVm.Correta);

                questao.AdicionarAlternativa(alternativa);
            }           

            await _questaoRepository.Adicionar(questao);

            if (!await _questaoRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar questão");

            return await Task.FromResult(new ServerStatus("Questão adicionada com sucesso!"));
        }

        public async Task<ServerStatus> Atualizar(QuestaoViewModel questaoVm)
        {
            var questao = await _questaoRepository.ObterQuestaoComAlternativas(questaoVm.QuestaoId);

            var disciplina = await _disciplinaRepository.ObterPorId(questaoVm.DisciplinaId);

            questao.AtualizarQuestao(questaoVm.Descricao, disciplina);

            if (!questao.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(questao.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }
            _questaoRepository.Atualizar(questao);

            var alternativasAntigas = questao.Alternativas.Select(x => x.Id).ToList();
            await _serviceQuestao.RemoverAlternativas(alternativasAntigas);

            //Adicionar as alternativas novas
            foreach (var alternativaVm in questaoVm.Alternativas)
            {
                var alternativa = new Alternativa(questao.Id, alternativaVm.Descricao,
                    alternativaVm.Posicao, alternativaVm.Correta);

                await _serviceQuestao.AdicionarAlternativa(alternativa);
            }

            if (!await _questaoRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar questão");

            return await Task.FromResult(new ServerStatus("Questão atualizada com sucesso!"));
        }

        public async Task<ServerStatus> RemoverQuestao(Guid questaoId)
        {
            var questao = await _questaoRepository.ObterPorId(questaoId);
            questao.EnviarParaLixeira();

            _questaoRepository.Atualizar(questao);

            if (!await _questaoRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar questão");

            return await Task.FromResult(new ServerStatus("Questão removida com sucesso!"));
        }

        public async Task<QuestaoViewModel> ObterQuestaoPorId(Guid questaoId)
        {
            var questao = await _questaoRepository.ObterQuestaoComAlternativas(questaoId);
            return QuestaoViewModel.Mapear(questao);
        }

        public async Task<List<QuestaoViewModel>> ObterTodasQuestoes()
        {
            var questoes = await _questaoRepository.ObterTodasQuestoesComAlternativas();
            return questoes.Select(QuestaoViewModel.Mapear).ToList();
        }

        public async Task<List<QuestaoViewModel>> ObterQuestoesPorDisciplina(Guid disciplinaId)
        {
            var questoes = await _questaoRepository.ObterQuestoesPorDisciplina(disciplinaId);
            return _mapper.Map<List<QuestaoViewModel>>(questoes);
        }


        public async Task<List<DisciplinaQuestaoViewModel>> ObterTotalQuestaoPorDisciplina()
        {
            var disciplinas = await _disciplinaRepository.ObterQuantidadeDeQuestaoPorDisciplina();
            return  _mapper.Map<List<DisciplinaQuestaoViewModel>>(disciplinas);
        }

        public async Task<ServerStatus> ObterQuestoesParaRealizar(Guid disciplinaId, int quantidade)
        {

            if (quantidade <= 0)
            {
                var erros = new List<string>();
                erros.Add("Informe a quantidade de questões");

               return await Task.FromResult(new ServerStatus(erros));
            }

            if (disciplinaId == null || disciplinaId == Guid.Empty)
            {
                var erros = new List<string>();
                erros.Add("Informe a disciplina");

                return await Task.FromResult(new ServerStatus(erros));
            }            

            var questoes = await _questaoRepository.ObterQuestoesParaRealizar(disciplinaId, quantidade);
            var questoesVm = questoes.Select(QuestaoViewModel.Mapear).ToList();

            return await Task.FromResult(new ServerStatus(questoesVm));
        }
    }
}
