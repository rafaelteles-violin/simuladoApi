using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IRespostaApplication
    {
        Task<ServerStatus> Adicionar(List<RespostaViewModel> respostaVm);
        Task<ServerStatus> Atualizar(List<RespostaViewModel> respostaVm);
        Task<List<RespostaViewModel>> ObterRespostaPorIdentificador(string identificador);
        Task<List<RespostaViewModel>> ObterTodasRespostasDoUsuario(Guid usuarioId);
    }
}
