using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("TB_NOTIFICACAO");

            builder.HasKey(n => n.IdNotificacao);

            builder.Property(n => n.IdNotificacao).HasColumnName("ID_NOTIFICACAO");

            builder.Property(n => n.IdTutor).HasColumnName("ID_TUTOR").IsRequired();

            builder.Property(n => n.IdPet).HasColumnName("ID_PET").IsRequired();

            builder.Property(n => n.TituloNotificacao)
                .HasColumnName("TITULO_NOTIFICACAO")
                .HasMaxLength(150);

            builder.Property(n => n.MensagemNotificacao)
                .HasColumnName("MENSAGEM_NOTIFICACAO")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(n => n.TipoNotificacao)
                .HasColumnName("TIPO_NOTIFICACAO")
                .HasMaxLength(50);

            builder.Property(n => n.DataEnvioNotificacao)
                .HasColumnName("DATA_ENVIO_NOTIFICACAO")
                .IsRequired();

            builder.Property(n => n.StatusEnvioNotificacao)
                .HasColumnName("STATUS_ENVIO_NOTIFICACAO")
                .HasMaxLength(20)
                .HasDefaultValue("PENDENTE");

            // Configuração dos Relacionamentos
            builder.HasOne(n => n.Tutor)
                .WithMany()
                .HasForeignKey(n => n.IdTutor)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Pet)
                .WithMany()
                .HasForeignKey(n => n.IdPet)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}