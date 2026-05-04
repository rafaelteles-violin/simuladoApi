using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;

namespace Simulado.Infra.Repositories
{
    public class AlternativaRespostaRepository : RepositoryBase<AlternativaResposta>, IAlternativaRespostaRepository
    {
        private readonly SimuladoContext _context;
        public AlternativaRespostaRepository(SimuladoContext context) 
            : base(context)
        {
            _context = context;
        }
    }
}
