namespace FreshUp.Application.DTOs;

public class CategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<ProductResultDto> Products { get; set; }
}
