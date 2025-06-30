//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Extensions;
//using ProjectName.Interfaces.Contexts;
//using Mapster;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Features.Users.GetAllUsers;

//public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, PaginatedResult<GetUserResponse>>
//{
//    private readonly IApplicationDbContext _context;
    
//    public GetAllUsersHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }
//    public async Task<PaginatedResult<GetUserResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
//    {
//        var heroes = _context.Users
//            .WhereIf(!string.IsNullOrEmpty(request.FirstName), x => EF.Functions.Like(x.FirstName, $"%{request.FirstName}%"))
//            .WhereIf(!string.IsNullOrEmpty(request.LastName), x => EF.Functions.Like(x.LastName!, $"%{request.LastName}%"))
//            //.WhereIf(request.Age != null, x => x.Age == request.Age)
//            //.WhereIf(request.UserType != null, x => x.UserType == request.UserType)
//            //.WhereIf(!string.IsNullOrEmpty(request.Team), x => x.Team == request.Team)
//            //.WhereIf(!string.IsNullOrEmpty(request.Individuality), x => EF.Functions.Like(x.Individuality!, $"%{request.Individuality}%"))
//            ;
//        return await heroes.ProjectToType<GetUserResponse>()
//            .OrderBy(x => x.Name)
//            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
//    }
//}