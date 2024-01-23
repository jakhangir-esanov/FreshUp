using FreshUp.Domain.Commons;
using FreshUp.Domain.Enums;

namespace FreshUp.Domain.Entities;

public sealed class Inventory : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long Quantity { get; set; }
}
