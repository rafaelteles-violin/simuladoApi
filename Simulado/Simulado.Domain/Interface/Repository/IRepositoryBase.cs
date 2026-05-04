using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        IUnitOfWorks UnitOfWorks { get; }
        Task<TEntity> ObterPorId(Guid Id);
        Task Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Remover(TEntity entity);
    }
}
