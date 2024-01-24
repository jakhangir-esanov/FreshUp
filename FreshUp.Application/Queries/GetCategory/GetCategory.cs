
namespace FreshUp.Application.Queries.GetCategory;

public record GetCategoryQuery : IRequest<CategoryResultDto>
{
    public long Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryResultDto>
{

    public Task<CategoryResultDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
