using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class FichaClinicaMapping : IEntityTypeConfiguration<FichaClinica>
    {
        public void Configure(EntityTypeBuilder<FichaClinica> builder)
        {
            builder.ToTable("TB_FICHA_CLINICA");

            builder.HasKey(f => f.IdFichaClinica);

            builder.Property(f => f.IdFichaClinica)
                .HasColumnName("ID_FICHA_CLINICA");

            builder.Property(f => f.IdPet)
                .HasColumnName("ID_PET")
                .IsRequired();

            builder.Property(f => f.TipoSanguineo)
                .HasColumnName("TIPO_SANGUINEO")
                .HasMaxLength(10);

            builder.Property(f => f.AlergiasFichaClinica)
                .HasColumnName("ALERGIAS_FICHA_CLINICA")
                .HasMaxLength(300);

            builder.Property(f => f.DoencasCronicasFichaClinica)
                .HasColumnName("DOENCAS_CRONICAS_FICHA_CLINIC") // Mantendo o nome original do seu modelo
                .HasMaxLength(300);

            builder.Property(f => f.ObservacoesFichaClinica)
                .HasColumnName("OBSERVACOES_FICHA_CLINICA")
                .HasMaxLength(300);

            builder.Property(f => f.DataCriacaoFichaClinica)
                .HasColumnName("DATA_CRIACAO_FICHA_CLINICA");

            // Configuração do Relacionamento
            builder.HasOne(f => f.Pet)
                .WithMany()
                .HasForeignKey(f => f.IdPet)
                .OnDelete(DeleteBehavior.Cascade); // Se o pet for deletado, a ficha clínica deve ser deletada também
        }
    }
}