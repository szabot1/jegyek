using System.ComponentModel.DataAnnotations;

namespace jegyek.Models;

public record CreateGradeDto
{
    [Required]
    [Range(1, 5)]
    public required int Value { get; init; }

    [Required]
    public string Description { get; init; } = "";
}
