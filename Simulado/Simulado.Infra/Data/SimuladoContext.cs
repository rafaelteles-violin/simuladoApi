using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simulado.Domain;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.Infra.Data
{
    public class SimuladoContext : IdentityDbContext<IdentityUser>, IUnitOfWorks
    {

        public SimuladoContext(DbContextOptions<SimuladoContext> options)
            : base(options) { }
        public SimuladoContext() { }

        public DbSet<Alternativa> Alternativa { get; set; }
        public DbSet<AlternativaResposta> AlternativaResposta { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<RespostaSaldo> RespostaSaldo { get; set; }
        public DbSet<Igreja> Igreja { get; set; }
        public DbSet<UsuarioIgreja> UsuarioIgreja { get; set; }

        public async Task<bool> Commit()
        {
            var cetZone = ZonaDeTempo.ObterZonaDeTempo();

            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue =
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                    entry.Property("DataAlteracao").CurrentValue =
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
                }
            }

            return await SaveChangesAsync() > 0;
        }
    }
}
