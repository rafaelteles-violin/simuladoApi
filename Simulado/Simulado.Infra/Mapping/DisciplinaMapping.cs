using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplina");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
               .HasColumnName("Descricao")
               .HasMaxLength(150)
               .IsRequired();

            builder.Property(x => x.DataCadastro)
                .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");

            builder.Property(x => x.TipoDisciplina)
                .HasColumnName("TipoDisciplina");

            builder.Property(x => x.TotalExibicaoQuestao)
                .HasColumnName("TotalExibicaoQuestao");
        }
    }
}
