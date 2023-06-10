using Chat.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Entities;
public class Message:BaseEntity
{
   
    public string Subject { get; set; }
    public string Content { get; set; }
    public Guid? SenderId { get; set; }

    public Guid? ReceiverId { get; set; }

    public DateTime? DateSent { get; set; }

    public virtual User? Receiver { get; set; }

    public virtual User? Sender { get; set; }
}
    
