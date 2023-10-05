namespace jegyek.Models;

public record Grade
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public int Value { get; init; }
    public string Description { get; init; } = "";
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public GradeDto AsDto()
    {
        return new(Id, Value, Description, CreatedAt);
    }
}