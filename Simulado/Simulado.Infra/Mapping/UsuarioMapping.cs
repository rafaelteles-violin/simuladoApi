using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulado.Domain.Entity;

namespace Simulado.Infra.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
               .HasColumnName("Nome")
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(x => x.Email)
              .HasColumnName("Email")
              .HasMaxLength(250)
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
