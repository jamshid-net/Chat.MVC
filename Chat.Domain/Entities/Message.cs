using Chat.Domain.Common;

namespace Chat.Domain.Entities;
public class Message:BaseEntity
{
   
    public string Subject { get; set; }
    public string Content { get; set; }
    public DateTime DateSent { get; set; }

    public Guid FromUserId { get; set; }
    public virtual User? FromUser { get; set; }
    
    public Guid ToUserId { get; set; }
    public virtual User? ToUser { get; set; }
}
