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
    public class IgrejaMapping : IEntityTypeConfiguration<Igreja>
    {
        public void Configure(EntityTypeBuilder<Igreja> builder)
        {
            builder.ToTable("Igreja");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
              .HasColumnName("Nome")
              .HasMaxLength(200)
              .IsRequired();

            builder.Property(x => x.DataCadastro)
            .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");
        }
    }
}
