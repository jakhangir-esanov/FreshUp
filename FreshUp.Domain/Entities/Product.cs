using FreshUp.Domain.Commons;
using FreshUp.Domain.Enums;

namespace FreshUp.Domain.Entities;

public sealed class Product : Auditable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; } = 0;
    public Unit Unit { get; set; }
    public string Description { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
