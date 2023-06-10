using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.UseCases.Messages.Commands;
public class MessageUpdateCommand:IRequest<bool>
{
    public Guid Id { get; init; }
    public string Subject { get; init; }
    public string Content { get; init; }
}
   

public class MessageUpdateCommandHandler : IRequestHandler<MessageUpdateCommand, bool>
{
    private readonly IApplicationDbContext _context;
   

    public MessageUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
       
    }

    public async Task<bool> Handle(MessageUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity=  await _context.Messages.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Message), request.Id);

        entity.Subject = request.Subject;
        entity.Content = request.Content;
        return await  _context.SaveChangesAsync(cancellationToken)>0;
       
    }
}
