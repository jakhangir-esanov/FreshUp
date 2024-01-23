using FreshUp.Domain.Commons;
using FreshUp.Domain.Enums;

namespace FreshUp.Domain.Entities;

public sealed class Order : Auditable
{
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public double TotalAmount { get; set; }
    public Status Status { get; set; }

    public ICollection<OrderList> OrderLists { get; set; }  
}
