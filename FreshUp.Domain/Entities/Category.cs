using FreshUp.Domain.Commons;

namespace FreshUp.Domain.Entities;

public sealed class Category : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Product> Products { get; set; }
}
