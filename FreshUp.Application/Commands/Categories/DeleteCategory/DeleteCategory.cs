
namespace FreshUp.Application.Commands.Categories.DeleteCategory;

public record DeleteCategoryCommand : IRequest<bool>
{
    public DeleteCategoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IRepository<Category> repository;

    public DeleteCategoryCommandHandler(IRepository<Category> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Category was not found!");

        this.repository.Delete(category);
        await this.repository.SaveAsync();

        return true;
    }
}
