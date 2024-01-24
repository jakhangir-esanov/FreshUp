namespace FreshUp.Application.Commands.Categories.CreateCategory;

public record CreateCategoryCommand : IRequest<Category>
{
    public CreateCategoryCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    public readonly IRepository<Category> repository;

    public CreateCategoryCommandHandler(IRepository<Category> repository)
    {
        this.repository = repository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.SelectAsync(x => x.Name.Equals(request.Name));
        if (category is not null)
            throw new AlreadyExistException("Already exist!");

        var newCategory = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await this.repository.InsertAsync(newCategory);
        await this.repository.SaveAsync();

        return newCategory;
    }
}
