using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class ClinicaMapping : IEntityTypeConfiguration<Clinica>
    {
        public void Configure(EntityTypeBuilder<Clinica> builder)
        {
            builder.ToTable("TB_CLINICA");

            builder.HasKey(c => c.IdClinica);
            builder.Property(c => c.IdClinica).HasColumnName("ID_CLINICA");

            builder.Property(c => c.NomeFantasiaClinica).HasColumnName("NOME_FANTASIA_CLINICA").IsRequired().HasMaxLength(100);
            builder.Property(c => c.RazaoSocialClinica).HasColumnName("RAZAO_SOCIAL_CLINICA").IsRequired().HasMaxLength(150);
            builder.Property(c => c.CnpjClinica).HasColumnName("CNPJ_CLINICA").IsRequired().HasMaxLength(18);
            builder.Property(c => c.TelefoneClinica).HasColumnName("TELEFONE_CLINICA").IsRequired().HasMaxLength(20);
            builder.Property(c => c.EmailClinica).HasColumnName("EMAIL_CLINICA").IsRequired().HasMaxLength(150);
            builder.Property(c => c.CepClinica).HasColumnName("CEP_CLINICA").IsRequired().HasMaxLength(10);
            builder.Property(c => c.RuaClinica).HasColumnName("RUA_CLINICA").IsRequired().HasMaxLength(150);
            builder.Property(c => c.NumeroClinica).HasColumnName("NUMERO_CLINICA").IsRequired().HasMaxLength(20);
            builder.Property(c => c.ComplementoClinica).HasColumnName("COMPLEMENTO_CLINICA").HasMaxLength(100);
            builder.Property(c => c.BairroClinica).HasColumnName("BAIRRO_CLINICA").IsRequired().HasMaxLength(100);
            builder.Property(c => c.CidadeClinica).HasColumnName("CIDADE_CLINICA").IsRequired().HasMaxLength(100);
            builder.Property(c => c.EstadoClinica).HasColumnName("ESTADO_CLINICA").IsRequired().HasMaxLength(100);
            builder.Property(c => c.DataCadastroClinica).HasColumnName("DATA_CADASTRO_CLINICA").IsRequired();
        }
    }
}