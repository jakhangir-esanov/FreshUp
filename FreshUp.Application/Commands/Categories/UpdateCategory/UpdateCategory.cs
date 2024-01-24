
namespace FreshUp.Application.Commands.Categories.UpdateCategory;

public record UpdateCategoryCommand : IRequest<Category>
{
    public UpdateCategoryCommand(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly IRepository<Category> repository;

    public UpdateCategoryCommandHandler(IRepository<Category> repository)
    {
        this.repository = repository;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Category was not found!");

        category.Name = request.Name;
        category.Description = request.Description;

        this.repository.Update(category);
        await this.repository.SaveAsync();

        return category;
    }
}
