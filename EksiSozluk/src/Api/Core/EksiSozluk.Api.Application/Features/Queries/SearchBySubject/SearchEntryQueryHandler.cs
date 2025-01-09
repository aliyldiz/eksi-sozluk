using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.SearchBySubject;

public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IEntryRepository _repository;

    public SearchEntryQueryHandler(IEntryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        var result = _repository.Get(i => EF.Functions.Like(i.Subject, $"{request.SearchText}%"))
            .Select(i => new SearchEntryViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
            });
        
        return await result.ToListAsync(cancellationToken);
    }
}