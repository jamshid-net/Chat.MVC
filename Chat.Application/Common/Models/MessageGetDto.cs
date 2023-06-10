using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Models;
public class MessageGetDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; } 
    public string Content { get; set; } 

    public Guid SenderId { get; set; } 
    public Guid ReceiverId { get; set; }

    public DateTime? DateSent { get; set; }

}
