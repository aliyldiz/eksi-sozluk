using MediatR;

namespace EksiSozluk.Common.Models.RequestModels;

public class CreateEntryCommand : IRequest<bool>, IRequest<Guid>
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public Guid? CreatedById { get; set; }

    public CreateEntryCommand()
    {
        
    }

    public CreateEntryCommand(string subject, string content, Guid? createdById)
    {
        Subject = subject;
        Content = content;
        CreatedById = createdById;
    }
}