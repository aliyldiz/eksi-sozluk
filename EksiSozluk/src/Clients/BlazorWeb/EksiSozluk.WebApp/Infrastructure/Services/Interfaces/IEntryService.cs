using EksiSozluk.Common.ViewModels.Page;
using EksiSozluk.Common.ViewModels.Queries;
using EksiSozluk.Common.ViewModels.RequestModels;

namespace EksiSozluk.WebApp.Infrastructure.Services;

public interface IEntryService
{
    Task<List<GetEntriesViewModel>> GetEntries();
    Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
    Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
    Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);
    Task<PagedViewModel<GetEntryCommentViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
    Task<Guid> CreateEntry(CreateEntryCommand command);
    Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
    Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
}