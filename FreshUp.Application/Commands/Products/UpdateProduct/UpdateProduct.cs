

namespace FreshUp.Application.Commands.Products.UpdateProduct;

public record UpdateProductCommand : IRequest<Product>
{
    public UpdateProductCommand(long id, string name, double price, double volume, Domain.Enums.Unit unit, string description, long categoryId)
    {
        Id = id;
        Name = name;
        Price = price;
        Volume = volume;
        Unit = unit;
        Description = description;
        CategoryId = categoryId;
    }

    public long Id {  get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; }
    public Domain.Enums.Unit Unit { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IRepository<Product> repository;

    public UpdateProductCommandHandler(IRepository<Product> repository)
    {
        this.repository = repository;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Product was not found!");

        product.Name = request.Name;
        product.Price = request.Price;
        product.Volume = request.Volume;
        product.Unit = request.Unit;
        product.Description = request.Description;
        product.CategoryId = request.CategoryId;

        this.repository.Update(product);
        await this.repository.SaveAsync();

        return product;
    }
}
