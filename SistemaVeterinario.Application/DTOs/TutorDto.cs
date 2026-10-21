namespace SistemaVeterinario.Application.DTOs;

public record TutorDto(
    decimal IdTutor,
    string NomeTutor,
    string CpfTutor,
    string TelefoneTutor,
    string EmailTutor,
    string CepTutor,
    string RuaTutor,
    string NumeroTutor,
    string? ComplementoTutor,
    string BairroTutor,
    string CidadeTutor,
    string EstadoTutor,
    DateTime DataCadastroTutor,
    List<LinkDto> Links
);