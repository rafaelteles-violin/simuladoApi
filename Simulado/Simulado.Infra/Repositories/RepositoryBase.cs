using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly SimuladoContext _context;

        public RepositoryBase(SimuladoContext context)
        {
            _context = context;
        }

        public IUnitOfWorks UnitOfWorks => _context;

        public async Task Adicionar(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Atualizar(TEntity entity)
        {
            _context.Update(entity);
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Remover(TEntity entity)
        {
             _context.Remove(entity);
        }
    }
}
