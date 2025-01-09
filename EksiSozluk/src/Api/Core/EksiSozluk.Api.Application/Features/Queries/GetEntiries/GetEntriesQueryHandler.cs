using AutoMapper;
using AutoMapper.QueryableExtensions;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntiries;

public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;
    
    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();
        
        if (request.TodayEntries)
        {
            query = query.Where(i => i.CreateDate >= DateTime.Now.Date)
                .Where(i => i.CreateDate <= DateTime.Now.AddDays(1).Date);
        }
        
        query.Include(i => i.EntryComments);
        
        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(i => Guid.NewGuid()).Take(request.Count).ToListAsync(cancellationToken);
        
    }
}