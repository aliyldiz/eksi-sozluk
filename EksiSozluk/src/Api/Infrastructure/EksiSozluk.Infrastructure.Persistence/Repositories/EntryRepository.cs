using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Infrastructure.Persistence.Repositories;

public class EntryRepository : GenericRepository<Entry>, IEntryRepository
{
    public EntryRepository(EksiSozlukContext context) : base(context)
    {
    }
}