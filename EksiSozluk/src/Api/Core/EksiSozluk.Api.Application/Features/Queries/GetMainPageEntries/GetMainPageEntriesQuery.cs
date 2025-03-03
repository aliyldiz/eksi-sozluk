using EksiSozluk.Common.Models.Page;
using EksiSozluk.Common.Models.Queries;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQuery : BasePageQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
{
    public GetMainPageEntriesQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        UserId = userId;
    }

    public Guid? UserId { get; set; }
}

