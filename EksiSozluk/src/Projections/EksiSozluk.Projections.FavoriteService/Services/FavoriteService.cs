using Dapper;
using EksiSozluk.Common.Events.Entry;
using EksiSozluk.Common.Events.EntryComment;
using Microsoft.Data.SqlClient;

namespace EksiSozluk.Projections.FavoriteService.Services;

public class FavoriteService
{
    private readonly string connectionString;

    public FavoriteService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateEntryFav(CreateEntryFavEvent @event)
    {
        using var connection = new SqlConnection(connectionString);

        await connection
            .ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, CreatedById, CreateDate) VALUES(@Id, @EntryId, @CreatedById, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = @event.EntryId,
                    CreatedById = @event.CreatedBy
                });
    }
    
    public async Task CreateEntryCommentFav(CreateEntryCommentFavEvent @event)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync("INSERT INTO EntryCommentFavorite (Id, EntryCommentId, CreatedById, CreateDate) VALUES(@Id, @EntryCommentId, @CreatedById, GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.EntryCommentId,
                CreatedById = @event.CreatedBy
            });
    }

    public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE EntryId = @EntryId AND CreatedById = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById = @event.CreatedBy
            });
    }

    public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE EntryCommentId = @EntryCommentId AND CreatedById = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.EntryCommentId,
                CreatedById = @event.CreatedBy
            });
    }
}