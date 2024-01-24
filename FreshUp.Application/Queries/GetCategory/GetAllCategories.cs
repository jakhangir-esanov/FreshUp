
namespace FreshUp.Application.Queries.GetCategory;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResultDto>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>
{
    private readonly IRepository<Category> repository;
    private readonly IMapper mapper;
    public GetAllCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CategoryResultDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CategoryResultDto>>(categories);
        return Task.FromResult(res);
    }
}
