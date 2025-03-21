namespace EksiSozluk.Common.Models.Queries;

public class GetEntryDetailViewModel : BaseFooterRateFavoriteViewModel
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByUserName { get; set; }
}