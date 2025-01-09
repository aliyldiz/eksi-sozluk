namespace EksiSozluk.Common.ViewModels.Queries;

public class GetEntryCommentViewModel : BaseFooterRateFavoriteViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByUserName { get; set; }
}