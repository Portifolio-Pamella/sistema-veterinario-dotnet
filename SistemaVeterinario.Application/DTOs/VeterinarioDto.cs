namespace SistemaVeterinario.Application.DTOs;

public record VeterinarioDto(
    decimal IdVeterinario,
    string NomeVeterinario,
    string CrmVeterinario,
    string EspecialidadeVeterinario,
    string TelefoneVeterinario,
    string EmailVeterinario,
    string StatusVeterinario,
    DateTime DataCadastroVeterinario,
    List<LinkDto> Links
);