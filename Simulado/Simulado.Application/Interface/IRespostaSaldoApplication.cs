using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IRespostaSaldoApplication
    {
        Task<ServerStatus> AdicionarRespostaSaldo(RespostaSaldoViewModel respostaVm);
        Task<List<RespostaSaldoGetViewModel>> ObterRespostaSaldo(List<Guid> idsIgreja);
        Task<List<RespostaSaldoAvaliacaoViewModel>> ObterRespostaSaldoAvaliacao();
    }
}
