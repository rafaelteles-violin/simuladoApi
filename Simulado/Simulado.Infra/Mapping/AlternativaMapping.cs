using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class AlternativaMapping : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.ToTable("Alternativa");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
               .HasColumnName("Descricao")
               .IsRequired();

            builder.Property(x => x.Correta)
                .HasColumnName("Correta")
                .IsRequired();

            builder.Property(x => x.Posicao)
                .HasColumnName("Posicao")
                .IsRequired();

            builder.Property(x => x.DataCadastro)
             .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");

            //Relacionamento
            builder.HasOne(a => a.Questao)
                .WithMany(q => q.Alternativas)
                .HasForeignKey(a => a.QuestaoId);
        }
    }
}
