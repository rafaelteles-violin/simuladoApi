using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class AlternativaRespostaMapping : IEntityTypeConfiguration<AlternativaResposta>
    {
        public void Configure(EntityTypeBuilder<AlternativaResposta> builder)
        {
            builder.ToTable("AlternativaResposta");
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

            builder.Property(x => x.Selecionada)
                .HasColumnName("Selecionada");

            //Relacionamento
            builder.HasOne(a => a.Resposta)
                .WithMany(r => r.RespostaAlternativas)
                .HasForeignKey(a => a.RespostaId);
        }
    }
}
