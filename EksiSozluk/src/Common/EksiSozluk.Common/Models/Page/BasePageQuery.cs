namespace EksiSozluk.Common.ViewModels.Page;

public class BasePageQuery
{
    public BasePageQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
}