﻿
namespace FreshUp.Application.Commands.Products.CreateProduct;

public record CreateProductCommand : IRequest<Product>
{
    public CreateProductCommand(string name, double price, double volume, Domain.Enums.Unit unit, string description, long categoryId)
    {
        Name = name;
        Price = price;
        Volume = volume;
        Unit = unit;
        Description = description;
        CategoryId = categoryId;
    }

    public string Name { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; }
    public Domain.Enums.Unit Unit { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IRepository<Product> repository;

    public CreateProductCommandHandler(IRepository<Product> repository)
    {
        this.repository = repository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.repository.SelectAsync(x => x.Volume.Equals(request.Volume));
        if (product is not null)
            throw new AlreadyExistException("Product is already exist!");

        var newProduct = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Volume = request.Volume,
            Unit = request.Unit,
            Description = request.Description,
            CategoryId = request.CategoryId
        };

        await this.repository.InsertAsync(newProduct);
        await this.repository.SaveAsync();

        return newProduct;
    }
}
