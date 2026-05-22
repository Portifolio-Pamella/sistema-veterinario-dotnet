using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class HistoricoMapping : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            builder.ToTable("TB_HISTORICO");

            builder.HasKey(h => h.IdHistorico);

            builder.Property(h => h.IdHistorico)
                .HasColumnName("ID_HISTORICO");

            builder.Property(h => h.IdPet)
                .HasColumnName("ID_PET")
                .IsRequired();

            builder.Property(h => h.DescricaoHistorico)
                .HasColumnName("DESCRICAO_HISTORICO")
                .HasMaxLength(500);

            builder.Property(h => h.DataRegistroHistorico)
                .HasColumnName("DATA_REGISTRO_HISTORICO")
                .IsRequired();

            builder.Property(h => h.TipoEvento)
                .HasColumnName("TIPO_EVENTO")
                .HasMaxLength(100);

            // Relacionamento com Pet
            // Usamos Cascade pois o histórico pertence diretamente ao ciclo de vida do pet
            builder.HasOne(h => h.Pet)
                .WithMany()
                .HasForeignKey(h => h.IdPet)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}