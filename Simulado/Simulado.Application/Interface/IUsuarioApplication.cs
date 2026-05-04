using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IUsuarioApplication
    {
        Task<ServerStatus> Adicionar(UsuarioViewModel usuarioVm);
        Task<ServerStatus> Atualizar(UsuarioViewModel usuarioVm);
        Task<ServerStatus> AtualizarEmail(UsuarioViewModel usuarioVm);
        Task<ServerStatus> AtualizarSenha(UsuarioViewModel usuarioVm);
        Task<List<IgrejaViewModel>> ObterIgrejasDoUsuario(Guid usuarioId);

        Task<List<UsuarioViewModel>> ObterTodos();


    }
}
