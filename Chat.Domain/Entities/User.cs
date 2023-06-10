using Chat.Domain.Common;
using System.Text.Json.Serialization;

namespace Chat.Domain.Entities;
public class User:BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual ICollection<User>? Friends { get; set; }
}
