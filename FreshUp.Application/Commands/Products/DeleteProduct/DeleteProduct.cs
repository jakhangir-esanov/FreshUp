
namespace FreshUp.Application.Commands.Products.DeleteProduct;

public record DeleteProductCommand : IRequest<bool>
{
    public DeleteProductCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IRepository<Product> repository;

    public DeleteProductCommandHandler(IRepository<Product> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Product was not found!");

        this.repository.Delete(product);
        await this.repository.SaveAsync();

        return true;
    }
}
