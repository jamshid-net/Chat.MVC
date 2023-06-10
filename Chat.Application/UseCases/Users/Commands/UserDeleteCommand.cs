using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.UseCases.Users.Commands;
public class UserDeleteCommand:IRequest<bool>
{
    public Guid UserId { get; init; }
    
}
public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UserDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {

        var entity =await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
        if(entity is null) 
            throw new NotFoundException(nameof(User),request.UserId);
        
        _context.Users.Remove(entity);
        if( await _context.SaveChangesAsync(cancellationToken)>0)
            return true;
        return false;

    }
}
