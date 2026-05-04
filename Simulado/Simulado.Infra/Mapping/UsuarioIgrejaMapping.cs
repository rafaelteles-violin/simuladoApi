using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class UsuarioIgrejaMapping : IEntityTypeConfiguration<UsuarioIgreja>
    {
        public void Configure(EntityTypeBuilder<UsuarioIgreja> builder)
        {
            builder.ToTable("UsuarioIgreja");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataCadastro)
             .HasColumnName("DataCadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");

            builder.Property(x => x.Lixeira)
                .HasColumnName("Lixeira");

            //Relacionamento
            builder.HasOne(a => a.Usuario)
                .WithMany(q => q.UsuarioIgreja)
                .HasForeignKey(a => a.UsuarioId);

            builder.HasOne(a => a.Igreja)
                .WithMany(q => q.UsuarioIgreja)
                .HasForeignKey(a => a.IgrejaId);
        }
    }
}
