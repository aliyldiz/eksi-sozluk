using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;

namespace EksiSozluk.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EksiSozlukContext context) : base(context)
    {
    }
}