using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);
        builder.ToTable("entrycomment", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(ec => ec.CreatedBy)
            .WithMany(e => e.EntryComments)
            .HasForeignKey(ec => ec.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ec => ec.Entry)
            .WithMany(u => u.EntryComments)
            .HasForeignKey(ec => ec.EntryId);
    }
}