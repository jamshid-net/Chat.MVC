using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.UseCases.Messages.Commands;
public class MessageCreateCommand:IRequest<Guid>
{
    public string Subject { get; init; }
    public string Content { get; init; }
    public string ReceiverUserName { get; init; }
   
}
public class MessageCreateCommandHandler : IRequestHandler<MessageCreateCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public MessageCreateCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }
    public Task<Guid> Handle(MessageCreateCommand request, CancellationToken cancellationToken)
    {
        var receiver =  _context.Users.SingleOrDefault(x=> x.UserName ==request.ReceiverUserName);
        var sender = _currentUser.UserId;
        Message message = new Message
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            Subject = request.Subject,
            SenderId = sender,
            ReceiverId = receiver.Id,
            DateSent = DateTime.UtcNow,
        };
        return Task.FromResult(message.Id);

    }
}
