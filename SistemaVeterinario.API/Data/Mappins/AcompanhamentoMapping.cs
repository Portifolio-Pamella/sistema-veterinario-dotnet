using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class AcompanhamentoMapping : IEntityTypeConfiguration<Acompanhamento>
    {
        public void Configure(EntityTypeBuilder<Acompanhamento> builder)
        {
            builder.ToTable("TB_ACOMPANHAMENTO");

            builder.HasKey(a => a.IdAcompanhamento);

            builder.Property(a => a.IdAcompanhamento)
                .HasColumnName("ID_ACOMPANHAMENTO");

            builder.Property(a => a.NomeAcompanhamento)
                .HasColumnName("NOME_ACOMPANHAMENTO")
                .HasMaxLength(150);

            builder.Property(a => a.DescricaoAcompanhamento)
                .HasColumnName("DESCRICAO_ACOMPANHAMENTO")
                .HasMaxLength(500);

            builder.Property(a => a.DataInicioAcompanhamento)
                .HasColumnName("DATA_INICIO_ACOMPANHAMENTO")
                .IsRequired();

            builder.Property(a => a.StatusAcompanhamento)
                .HasColumnName("STATUS_ACOMPANHAMENTO")
                .HasMaxLength(20)
                .HasDefaultValue("ATIVO");

            // Configuração dos Relacionamentos
            builder.HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.IdPet)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Veterinario)
                .WithMany()
                .HasForeignKey(a => a.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}