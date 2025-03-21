using EksiSozluk.Common.Models.Queries;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; }
}