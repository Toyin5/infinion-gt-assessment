using Core.Enums;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; } = 0;
    public string UserId { get; set; }
    public Status Status { get; set; }
}
