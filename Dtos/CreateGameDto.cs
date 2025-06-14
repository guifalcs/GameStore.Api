using System.ComponentModel.DataAnnotations;

namespace Gamestore.Api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
    );