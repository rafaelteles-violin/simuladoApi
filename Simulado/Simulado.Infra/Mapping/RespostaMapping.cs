using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class RespostaMapping : IEntityTypeConfiguration<Resposta>
    {
        public void Configure(EntityTypeBuilder<Resposta> builder)
        {
            builder.ToTable("Resposta");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataCadastro)
             .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");

            builder.Property(x => x.Aluno)
                .HasColumnName("Aluno")
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(x => x.Identificador)
              .HasColumnName("Identificador");

            //Relacionamento
            builder.HasOne(r => r.Questao)
                .WithMany(q => q.Respostas)
                .HasForeignKey(r => r.QuestaoId);
        }
    }
}
