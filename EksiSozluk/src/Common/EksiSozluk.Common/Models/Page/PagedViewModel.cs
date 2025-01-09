namespace EksiSozluk.Common.ViewModels.Page;

public class PagedViewModel<T> where T : class
{
    public PagedViewModel() : this (new List<T>(), new Page())
    {
        
    }
    
    public PagedViewModel(IList<T> result, Page pageInfo)
    {
        Result = result;
        PageInfo = pageInfo;
    }

    public IList<T> Result { get; set; }
    public Page PageInfo { get; set; }
}