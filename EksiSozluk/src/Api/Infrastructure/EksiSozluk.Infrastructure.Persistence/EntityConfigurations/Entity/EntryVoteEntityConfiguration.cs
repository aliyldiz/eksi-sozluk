using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.Entity;

public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entryvote", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(ev => ev.Entry)
            .WithMany(e => e.EntryVotes)
            .HasForeignKey(ev => ev.EntryId);
    }
}