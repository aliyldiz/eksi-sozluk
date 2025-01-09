using MediatR;

namespace EksiSozluk.Common.ViewModels.Queries;

public class SearchEntryQuery : IRequest<List<SearchEntryViewModel>>
{
    public SearchEntryQuery()
    {
        
    }
    
    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }

    public string SearchText { get; set; }
}