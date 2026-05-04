using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Service
{
    public class ServiceResposta : IServiceResposta
    {
        private readonly IAlternativaRespostaRepository _alternativaRespostaRepository;
        public ServiceResposta(IAlternativaRespostaRepository alternativaRespostaRepository)
        {
            _alternativaRespostaRepository = alternativaRespostaRepository;
        }

        public async Task AdicionarAlternativaResposta(AlternativaResposta alternativa)
        {
            await _alternativaRespostaRepository.Adicionar(alternativa);
        }

        public async Task RemoverAlternativaResposta(List<Guid> alternativasId)
        {
            foreach (var alternativaId in alternativasId)
            {
                var alternativa = await _alternativaRespostaRepository.ObterPorId(alternativaId);
                await _alternativaRespostaRepository.Remover(alternativa);
            }
        }       
    }
}
