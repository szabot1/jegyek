using System.ComponentModel.DataAnnotations;

namespace jegyek.Models;

public record UpdateGradeDto
{
    [Required]
    [Range(1, 5)]
    public int Value { get; init; }

    [Required]
    public string Description { get; init; } = "";
}