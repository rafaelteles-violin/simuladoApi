using AutoMapper;
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
    public class RespostaSaldoApplication(IRespostaSaldoRepository respostaSaldoRepository,
                                        IMapper mapper) : IRespostaSaldoApplication
    {

        public async Task<ServerStatus> AdicionarRespostaSaldo(RespostaSaldoViewModel respostaVm)
        {
            var respostaSaldo = mapper.Map<RespostaSaldo>(respostaVm);

            await respostaSaldoRepository.Adicionar(respostaSaldo);

            if (!await respostaSaldoRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar resposta");

            return await Task.FromResult(new ServerStatus("Resposta adicionada com sucesso!"));
        }

        public async Task<List<RespostaSaldoGetViewModel>> ObterRespostaSaldo(List<Guid> idsIgreja)
        {
            List<RespostaSaldo> respostas;

            if (idsIgreja?.Count == 0 || idsIgreja is null)
                respostas = await respostaSaldoRepository.ObterRespostaSaldo();

            else
                respostas = await respostaSaldoRepository.ObterRespostaSaldoPorIgrejas(idsIgreja);

            return mapper.Map<List<RespostaSaldoGetViewModel>>(respostas);
        }

        public async Task<List<RespostaSaldoAvaliacaoViewModel>> ObterRespostaSaldoAvaliacao()
        {
            var respostas = await respostaSaldoRepository.ObterRespostaSaldoAvaliacao();

            return respostas
                .GroupBy(x => x.DisciplinaId)
                .Select(g => new RespostaSaldoAvaliacaoViewModel
                {
                    Disciplina = g.First().Disciplina.Descricao,
                    Data = g.First().DataCadastro.ToString("dd/MM/yyyy HH:mm"),

                    RespostaSaldoAvaliacaoDetalhe = g.Select(x => new RespostaSaldoAvaliacaoDetalheViewModel
                    {
                        NomeCandidato = x.NomeCandidato,
                        TotalAcerto = x.TotalAcerto,
                        TotalErro = x.TotalErro,
                        TotalQuestao = x.TotalQuestao,
                        Disciplina = x.Disciplina.Descricao,
                        DataCadastro = x.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
                        Identificador = x.Identificador,
                        TipoDisciplina = x.Disciplina.TipoDisciplina.ToString()
                    }).ToList()

                })
                .ToList();
        }
    }
}
