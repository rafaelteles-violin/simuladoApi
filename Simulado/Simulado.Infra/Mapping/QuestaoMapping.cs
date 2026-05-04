using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class QuestaoMapping : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.ToTable("Questao");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
               .HasColumnName("Descricao")
               .IsRequired();

            builder.Property(x => x.DataCadastro)
             .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");

            //Relacionamento
            builder.HasOne(q => q.Disciplina)
                .WithMany(d => d.Questoes)
                .HasForeignKey(a => a.DisciplinaId);
        }
    }
}
