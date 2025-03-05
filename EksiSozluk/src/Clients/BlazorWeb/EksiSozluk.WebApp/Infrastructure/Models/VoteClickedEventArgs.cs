namespace EksiSozluk.WebApp.Infrastructure.Models;

public class VoteClickedEventArgs
{
    public Guid EntryId { get; set; }
    public bool IsUpVoteClicked { get; set; }
    public bool UpVoteDeleted { get; set; }
    public bool IsDownVoteClicked { get; set; }
    public bool DownVoteDeleted { get; set; }
}