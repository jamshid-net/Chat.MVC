using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.UseCases.Users.Commands;
public class UserCreateCommand:IRequest<Guid>
{
    public string UserName { get; init; } 
    public string Email { get; init; }  
    public string Password { get; init; }
}
public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UserCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = request.Password,
            UserName = request.UserName,
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    
    }
}
