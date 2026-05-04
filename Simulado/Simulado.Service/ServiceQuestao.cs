using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Service
{
    public class ServiceQuestao : IServiceQuestao
    {
        private readonly IAlternativaRepository _alternativaRepository;
        public ServiceQuestao(IAlternativaRepository alternativaRepository)
        {
            _alternativaRepository = alternativaRepository;
        }
        public async Task AdicionarAlternativa(Alternativa alternativa)
        {
            await _alternativaRepository.Adicionar(alternativa);
        }

        public async Task RemoverAlternativas(List<Guid> alternativasId)
        {
            foreach (var alternativaId in alternativasId)
            {
                var alternativa = await _alternativaRepository.ObterPorId(alternativaId);
                await _alternativaRepository.Remover(alternativa);
            }
        }
    }
}
