using FreshUp.Domain.Commons;

namespace FreshUp.Domain.Entities;

public sealed class Inventory : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long Quantity { get; set; }
}
