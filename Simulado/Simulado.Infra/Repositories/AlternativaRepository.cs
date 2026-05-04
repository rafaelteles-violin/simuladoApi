using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.Infra.Repositories
{
    public class AlternativaRepository : RepositoryBase<Alternativa>, IAlternativaRepository
    {
        private readonly SimuladoContext _context;
        public AlternativaRepository(SimuladoContext context) 
            : base(context)
        {
            _context = context;
        }

        public async Task<List<Alternativa>> ObterAlternativasDaQuestao(Guid questaoId)
        {
            return await _context.Alternativa.Where(x => x.QuestaoId == questaoId).ToListAsync();
        }
    }
}
