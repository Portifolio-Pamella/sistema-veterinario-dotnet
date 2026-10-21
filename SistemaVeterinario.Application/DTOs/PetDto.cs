namespace SistemaVeterinario.Application.DTOs;

public record PetDto(
    decimal IdPet,
    decimal IdTutor,
    string NomePet,
    string EspeciePet,
    string RacaPet,
    string SexoPet,
    DateTime DataNascimentoPet,
    decimal PesoPet,
    string CorPet,
    DateTime DataCadastroPet,
    List<LinkDto> Links
);