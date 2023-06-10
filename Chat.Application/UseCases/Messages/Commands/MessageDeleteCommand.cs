using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.UseCases.Messages.Commands;
public class MessageDeleteCommand:IRequest<bool>
{
    public Guid Id { get; init; }
}
public class MessageDeleteCommandHandler : IRequestHandler<MessageDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public MessageDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(MessageDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity =await _context.Messages.FindAsync(new object[] { request.Id });
        if(entity is null)
            throw new NotFoundException(nameof(Message),request.Id);

        _context.Messages.Remove(entity);

        return  await _context.SaveChangesAsync(cancellationToken)>0;
    }
}
