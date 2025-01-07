using System.Reflection.Metadata;
using Bogus;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EksiSozluk.Infrastructure.Persistence.Context;

public class SeedData
{
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
                .RuleFor(u => u.Id, u => Guid.NewGuid())
                .RuleFor(u => u.CreateDate,
                    u => u.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(u => u.FirstName, u => u.Person.FirstName)
                .RuleFor(u => u.LastName, u => u.Person.LastName)
                .RuleFor(u => u.EmailAddress, u => u.Internet.Email())
                .RuleFor(u => u.UserName, u => u.Internet.UserName())
                .RuleFor(u => u.Password, u => PasswordEncryptor.Encrypt(u.Internet.Password()))
                .RuleFor(u => u.EmailConfirmed, u => u.Random.Bool())
            .Generate(500);

        return result;
    }
    
    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        
        var context = new EksiSozlukContext(dbContextBuilder.Options);

        if (context.Users.Any())
        {
            await Task.CompletedTask;
            return;
        }
        
        var users = GetUsers();
        var userIds = users.Select(i => i.Id);
        
        await context.Users.AddRangeAsync(users);
        
        var guids = Enumerable.Range(0, 150).Select(e => Guid.NewGuid()).ToList();
        int counter = 0;
        
        var entries = new Faker<Entry>("tr")
                .RuleFor(e => e.Id, e => guids[counter++])
                .RuleFor(e => e.CreateDate, 
                    e => e.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(e => e.Subject, e => e.Lorem.Sentence(5, 5))
                .RuleFor(e => e.Content, e => e.Lorem.Paragraphs(2))
                .RuleFor(e => e.CreatedById, e => e.PickRandom(userIds))
            .Generate(150);
        
        await context.Entries.AddRangeAsync(entries);
        
        var comments = new Faker<EntryComment>("tr")
                .RuleFor(c => c.Id, c => Guid.NewGuid())
                .RuleFor(c => c.CreateDate,
                    c => c.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(c => c.Content, c => c.Lorem.Paragraphs(2))
                .RuleFor(c => c.CreatedById, c => c.PickRandom(userIds))
                .RuleFor(c => c.EntryId, c => c.PickRandom(guids))
            .Generate(1000);
        
        await context.EntryComments.AddRangeAsync(comments);
        
        await context.SaveChangesAsync();
    }
}