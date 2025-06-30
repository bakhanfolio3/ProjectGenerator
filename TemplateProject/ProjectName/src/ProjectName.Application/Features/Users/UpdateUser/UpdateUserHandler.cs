//using ProjectName.Application.Common;
//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Domain;
//using ProjectName.Interfaces.Contexts;
//using Mapster;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Features.Users.UpdateUser;

//public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, IResult<GetUserResponse>>
//{
//    private readonly IApplicationDbContext _context;

//    public UpdateUserHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IResult<GetUserResponse>> Handle(UpdateUserRequest request,
//        CancellationToken cancellationToken)
//    {
//        var originalHero = await _context.Users
//            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
//        if (originalHero == null) return Result<GetUserResponse>.NotFound();

//        originalHero.FirstName = request.FirstName;
//        originalHero.LastName = request.LastName;
//        //originalHero.Team = request.Team;
//        //originalHero.Individuality = request.Individuality;
//        //originalHero.Age = request.Age;
//        //originalHero.UserType = request.UserType;
//        _context.Users.Update(originalHero);
//        await _context.SaveChangesAsync(cancellationToken);
//        return originalHero.Adapt<IResult<GetUserResponse>>();
//    }
//}