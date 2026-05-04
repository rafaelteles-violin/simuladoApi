using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Interface
{
    public interface IIgrejaApplication
    {
        Task<ServerStatus> Adicionar(IgrejaViewModel igrejaVm);
        Task<List<IgrejaViewModel>> ObterTodos();
    }
}
