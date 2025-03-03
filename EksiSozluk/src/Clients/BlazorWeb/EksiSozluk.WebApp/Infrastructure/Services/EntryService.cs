using System.Net.Http.Json;
using EksiSozluk.Common.Models.Page;
using EksiSozluk.Common.Models.Queries;
using EksiSozluk.Common.Models.RequestModels;

namespace EksiSozluk.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient _httpClient;

    public EntryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var result = await _httpClient.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEnties=false&count=30");
        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var result = await _httpClient.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
    {
        var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryCommentViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");
        return result;
    }

    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var res = await _httpClient.PostAsJsonAsync("api/Entry/CreateEntry", command);
        if (!res.IsSuccessStatusCode)
            return Guid.Empty;
        
        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var res = await _httpClient.PostAsJsonAsync("api/Entry/CreateEntryComment", command);
        if (!res.IsSuccessStatusCode)
            return Guid.Empty;
        
        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var result = await _httpClient.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
        return result;
    }
}