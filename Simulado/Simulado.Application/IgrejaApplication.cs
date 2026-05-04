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
    public class IgrejaApplication(IIgrejaRepository igrejaRepository,
            IMapper mapper) : IIgrejaApplication
    {

        public async Task<ServerStatus> Adicionar(IgrejaViewModel igrejaVm)
        {
            var igreja = mapper.Map<Igreja>(igrejaVm);

            if (!igreja.ValidarEntidade().IsValid)
            {

                return await Task.FromResult(new ServerStatus(igreja.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            await igrejaRepository.Adicionar(igreja);

            if (!await igrejaRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar igreja");

            return await Task.FromResult(new ServerStatus("Igreja adicionada com sucesso!"));
        }

        public async Task<List<IgrejaViewModel>> ObterTodos()
        {
            var igrejas = await igrejaRepository.ObterIgrejas();

            return mapper.Map<List<IgrejaViewModel>>(igrejas);
        }
    }
}
