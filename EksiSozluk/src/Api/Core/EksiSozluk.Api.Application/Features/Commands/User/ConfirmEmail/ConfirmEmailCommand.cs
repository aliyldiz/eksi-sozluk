using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.User.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<bool>
{
    public Guid ConfirmationId { get; set; }
}