using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Infrastructure.Persistence.Context;
using EksiSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EksiSozluk.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrstructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EksiSozlukContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        var seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
        
        return services;
    }
}