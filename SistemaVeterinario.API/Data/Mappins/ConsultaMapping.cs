using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("TB_CONSULTA");

            builder.HasKey(c => c.IdConsulta);

            builder.Property(c => c.IdConsulta).HasColumnName("ID_CONSULTA");

            builder.Property(c => c.DataConsulta).HasColumnName("DATA_CONSULTA").IsRequired();

            builder.Property(c => c.MotivoConsulta).HasColumnName("MOTIVO_CONSULTA").HasMaxLength(200).IsRequired();

            builder.Property(c => c.SintomasConsulta).HasColumnName("SINTOMAS_CONSULTA").HasMaxLength(300).IsRequired();

            builder.Property(c => c.DiagnosticoConsulta).HasColumnName("DIAGNOSTICO_CONSULTA").HasMaxLength(300).IsRequired();

            builder.Property(c => c.RetornoConsulta).HasColumnName("RETORNO_CONSULTA");

            builder.Property(c => c.StatusConsulta).HasColumnName("STATUS_CONSULTA").HasMaxLength(50).HasDefaultValue("AGENDADA");

            builder.Property(c => c.ObservacoesConsulta).HasColumnName("OBSERVACOES_CONSULTA").HasMaxLength(300);

            // Relacionamentos e Restrições
            builder.HasOne(c => c.Clinica)
                .WithMany()
                .HasForeignKey(c => c.IdClinica)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Veterinario)
                .WithMany()
                .HasForeignKey(c => c.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Pet)
                .WithMany()
                .HasForeignKey(c => c.IdPet)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}