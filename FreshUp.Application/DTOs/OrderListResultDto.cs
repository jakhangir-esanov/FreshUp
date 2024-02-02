namespace FreshUp.Application.DTOs;

public class OrderListResultDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public double Quantity { get; set; }
    public double Volume { get; set; }
    public Domain.Enums.Unit Unit { get; set; }
    public double Price { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
}
