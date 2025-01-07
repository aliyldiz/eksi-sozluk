using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entrycommentvote", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(ecv => ecv.EntryComment)
            .WithMany(ec => ec.EntryCommentVotes)
            .HasForeignKey(ecv => ecv.EntryCommentId);
    }
}