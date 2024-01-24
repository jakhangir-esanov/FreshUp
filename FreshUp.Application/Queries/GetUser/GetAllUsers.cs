namespace FreshUp.Application.Queries.GetUser;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>
{
}

public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;
    public GetAllUserQueryHandler(IRepository<User> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<UserResultDto>>(users);
        return Task.FromResult(res);
    }
}
