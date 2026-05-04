using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Infra.Mapping
{
    public class RespostaSaldoMapping : IEntityTypeConfiguration<RespostaSaldo>
    {
        public void Configure(EntityTypeBuilder<RespostaSaldo> builder)
        {
            builder.ToTable("RespostaSaldo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeCandidato)
           .HasColumnName("NomeCandidato");

            builder.Property(x => x.TotalAcerto)
           .HasColumnName("Acerto");

            builder.Property(x => x.TotalErro)
           .HasColumnName("Erro");

            builder.Property(x => x.TotalQuestao)
           .HasColumnName("Questoes");

            builder.Property(x => x.DataCadastro)
             .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");


            builder.Property(x => x.Identificador)
                .HasColumnName("Identificador");

            //Relacionamento
            builder.HasOne(rs => rs.Disciplina)
                .WithMany(d => d.RespostaSaldo)
                .HasForeignKey(rs => rs.DisciplinaId);

            builder.HasOne(rs => rs.Igreja)
             .WithMany(d => d.RespostaSaldo)
             .HasForeignKey(rs => rs.IgrejaId);
        }
    }
}
