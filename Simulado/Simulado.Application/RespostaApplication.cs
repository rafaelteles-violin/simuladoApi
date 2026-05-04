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
using System.Threading.Tasks;

namespace Simulado.Application
{
    public class RespostaApplication : IRespostaApplication
    {
        private readonly IRespostaRepository _respostaRepository;
        private readonly IMapper _mapper;
        private readonly IServiceResposta _serviceResposta;

        public RespostaApplication(IRespostaRepository respostaRepository,
                                   IMapper mapper,
                                   IServiceResposta serviceResposta)
        {
            _respostaRepository = respostaRepository;
            _mapper = mapper;
            _serviceResposta = serviceResposta;
        }

        public async Task<ServerStatus> Adicionar(List<RespostaViewModel> respostasVm)
        {
            foreach (var respostaVm in respostasVm)
            {
                var resposta = new Resposta(respostaVm.QuestaoId, respostaVm.Aluno, respostaVm.Identificador);

                foreach (var alternativaVm in respostaVm.AlternativaResposta)
                {
                    var alternativa = new AlternativaResposta(resposta.Id, alternativaVm.Descricao,
                        alternativaVm.Posicao, alternativaVm.Correta, alternativaVm.Selecionada);

                    resposta.AdicionarAlternativaResposta(alternativa);
                }

                if (!resposta.ValidarEntidade().IsValid)
                {
                    return await Task.FromResult(new ServerStatus(resposta.ValidarEntidade()
                                     .Errors.Select(x => x.ErrorMessage).ToList()));
                }

                await _respostaRepository.Adicionar(resposta);
            }

            if (!await _respostaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar resposta");


            return await Task.FromResult(new ServerStatus("Resposta adicionada com sucesso!"));
        }

        public async Task<ServerStatus> Atualizar(List<RespostaViewModel> respostasVm)
        {
            foreach (var respostaVm in respostasVm)
            {
                var resposta = await _respostaRepository.ObterRespostaComAlternativas(respostaVm.RespostaId);

                var alternativasAntigas = resposta.RespostaAlternativas.Select(x => x.Id).ToList();
                await _serviceResposta.RemoverAlternativaResposta(alternativasAntigas);

                //Adicionar as alternativas novas
                foreach (var alternativaVm in respostaVm.AlternativaResposta)
                {
                    var alternativa = new AlternativaResposta(resposta.Id, alternativaVm.Descricao,
                        alternativaVm.Posicao, alternativaVm.Correta, alternativaVm.Selecionada);

                    await _serviceResposta.AdicionarAlternativaResposta(alternativa);
                }
            }

            //Sera que ele vai conseguir persistir varias respostas???
            if (!await _respostaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao atualizar resposta");

            return await Task.FromResult(new ServerStatus("Resposta atualizada com sucesso!"));
        }

        public async Task<List<RespostaViewModel>> ObterRespostaPorIdentificador(string identificador)
        {
            var result = await _respostaRepository.ObterRespostasPorIdentificador(identificador);

            return result.Select(RespostaViewModel.Mapear).ToList();
        }

        public Task<List<RespostaViewModel>> ObterTodasRespostasDoUsuario(Guid usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
