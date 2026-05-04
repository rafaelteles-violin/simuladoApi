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
    public class QuestaoRepository : RepositoryBase<Questao>, IQuestaoRepository
    {
        private readonly SimuladoContext _context;
        public QuestaoRepository(SimuladoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Questao> ObterQuestaoComAlternativas(Guid questaoId)
        {
            return await _context.Questao.Include(x => x.Alternativas)
                                   .Include(x => x.Disciplina)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.Id == questaoId && !x.Lixeira);
        }

        public async Task<List<Questao>> ObterTodasQuestoesComAlternativas()
        {
            return await _context.Questao.Include(x => x.Alternativas)
                                         .Include(x => x.Disciplina)
                                         .AsNoTracking()
                                         .Where(x => !x.Lixeira)
                                         .ToListAsync();
        }

        public async Task<List<Questao>> ObterQuestoesPorDisciplina(Guid disciplinaId)
        {
            return await _context.Questao.Include(x => x.Disciplina)
                                         .AsNoTracking()
                                         .Where(x => !x.Lixeira && x.Disciplina.Id == disciplinaId)
                                         .ToListAsync();
        }

        public async Task<List<Questao>> ObterQuestoesParaRealizar(Guid disciplinaId, int quantidade)
        {
            Random random = new Random();

            return await _context.Questao.Include(x => x.Disciplina)
                                         .Include(x=>x.Alternativas)
                                         .AsNoTracking()
                                         .Where(x => !x.Lixeira && x.Disciplina.Id == disciplinaId)
                                         .OrderBy(p => Guid.NewGuid())
                                         .Take(quantidade)
                                         .ToListAsync();
        }        
    }
}
