using EksiSozluk.Common.Models.Queries;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntiries;

public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
{
    public bool TodayEntries { get; set; }
    public int Count { get; set; } = 100;
    
}
