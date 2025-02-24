namespace EksiSozluk.Common;

public class SozlukConstants
{
    public const string RabbitMqHost = "localhost";
    public const string DefaultExchangeType = "direct";
    
    public const string UserExchangeName = "userExchange";
    public const string UserEmailChangedQueueName = "userEmailChangedQueue";
    
    public const string FaxExchangeName = "FavExchange";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
    
    public const string CreateEntryVoteQueueName = "CreateEntryFVoteQueue";
    
    public const string VoteExchangeName = "VoteExchange";
}