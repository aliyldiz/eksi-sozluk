using EksiSozluk.Infrastructure.Persistence.Context;
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
        
        return services;
    }
}