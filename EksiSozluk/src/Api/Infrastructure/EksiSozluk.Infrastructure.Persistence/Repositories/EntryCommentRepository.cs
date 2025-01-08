using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(EksiSozlukContext context) : base(context)
    {
    }
}