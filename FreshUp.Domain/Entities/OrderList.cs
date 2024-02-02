using FreshUp.Domain.Commons;
using FreshUp.Domain.Enums;

namespace FreshUp.Domain.Entities;

public sealed class OrderList : Auditable
{
    public string ProductName { get; set; }
    public double Quantity { get; set; }
    public double Volume { get; set; }
    public Unit Unit { get; set; }
    public double Price { get; set; }

    public long ProductId { get; set; }
    public Product Product { get; set; }

    public long OrderId { get; set; }
    public Order Order { get; set; }    
}
