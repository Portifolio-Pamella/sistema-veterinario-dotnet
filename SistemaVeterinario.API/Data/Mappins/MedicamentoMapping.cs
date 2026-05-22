using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class MedicamentoMapping : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("TB_MEDICAMENTO");

            builder.HasKey(m => m.IdMedicamento);

            builder.Property(m => m.IdMedicamento).HasColumnName("ID_MEDICAMENTO");

            builder.Property(m => m.IdPet).HasColumnName("ID_PET").IsRequired();

            builder.Property(m => m.NomeMedicamento)
                .HasColumnName("NOME_MEDICAMENTO")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(m => m.DosagemMedicamento)
                .HasColumnName("DOSAGEM_MEDICAMENTO")
                .HasMaxLength(100);

            builder.Property(m => m.FrequenciaMedicamento)
                .HasColumnName("FREQUENCIA_MEDICAMENTO")
                .HasMaxLength(100);

            builder.Property(m => m.DataInicioMedicamento)
                .HasColumnName("DATA_INICIO_MEDICAMENTO")
                .IsRequired();

            builder.Property(m => m.DataFimMedicamento).HasColumnName("DATA_FIM_MEDICAMENTO");

            builder.Property(m => m.StatusMedicamento)
                .HasColumnName("STATUS_MEDICAMENTO")
                .HasMaxLength(20)
                .HasDefaultValue("ATIVO");

            builder.Property(m => m.ObservacaoMedicamento)
                .HasColumnName("OBSERVACAO_MEDICAMENTO")
                .HasMaxLength(300);

            // Relacionamento com Pet
            builder.HasOne(m => m.Pet)
                .WithMany()
                .HasForeignKey(m => m.IdPet)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}