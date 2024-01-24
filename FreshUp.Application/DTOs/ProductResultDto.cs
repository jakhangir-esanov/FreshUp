namespace FreshUp.Application.DTOs;

public class ProductResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Domain.Enums.Unit Unit { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
}
