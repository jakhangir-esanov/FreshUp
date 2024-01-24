namespace FreshUp.Application.Queries.GetCategory;

public record GetCategoryQuery : IRequest<CategoryResultDto>
{
    public long Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryResultDto>
{
    private readonly IRepository<Category> repository;
    private readonly IMapper mapper;
    public GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CategoryResultDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "Products" })
            ?? throw new NotFoundException("Category was not found!");

        return mapper.Map<CategoryResultDto>(category);
    }
}
