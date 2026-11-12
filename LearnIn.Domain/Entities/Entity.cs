namespace LearnIn.Domain.Entities;

public abstract class Entity<T>
{
    public T Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}