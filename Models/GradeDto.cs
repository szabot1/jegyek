using System.ComponentModel.DataAnnotations;

namespace jegyek.Models;

public record GradeDto(Guid Id, int Value, string Description, DateTimeOffset CreatedAt);