//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Interfaces.Contexts;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Features.Users.DeleteUser;

//public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, IResult>
//{
//    private readonly IApplicationDbContext _context;
//    public DeleteUserHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }
//    public async Task<IResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
//    {
//        var hero = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
//        if (hero is null) return await Result.FailAsync("Not Found");
//        _context.Users.Remove(hero);
//        await _context.SaveChangesAsync(cancellationToken);
//        return Result.Success();
//    }
//}