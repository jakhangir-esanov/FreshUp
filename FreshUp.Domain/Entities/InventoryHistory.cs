using FreshUp.Domain.Commons;

namespace FreshUp.Domain.Entities;

public class InventoryHistory : Auditable
{
    public long ProductId { get; set; }
    public double Quantity { get; set; }
}
