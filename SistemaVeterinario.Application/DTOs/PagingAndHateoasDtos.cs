namespace SistemaVeterinario.Application.DTOs;

public record LinkDto(string Href, string Rel, string Method);

public record PagedResponse<T>(
    IEnumerable<T> Items,
    int PageNumber,
    int PageSize,
    int TotalPages,
    int TotalRecords,
    List<LinkDto> Links
);