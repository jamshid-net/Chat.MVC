using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Configurations;
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
            .HasForeignKey(d => d.ReceiverId); 

        builder.HasOne(d=> d.Sender).WithMany(p=> p.MessageSenders)
            .HasForeignKey(d=> d.SenderId);
       
       
    }
}
