namespace FreshUp.Application.DTOs;

public class OrderResultDto
{
    public long Id {  get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public Status Status { get; set; }
    public ICollection<OrderListResultDto> OrderLists { get; set; }
}
