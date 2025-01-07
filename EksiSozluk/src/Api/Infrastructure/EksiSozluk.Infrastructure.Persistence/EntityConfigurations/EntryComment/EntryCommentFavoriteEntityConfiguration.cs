using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);
        builder.ToTable("entrycommentfavorite", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(ecf => ecf.EntryComment)
            .WithMany(ec => ec.EntryCommentFavorites)
            .HasForeignKey(ecf => ecf.EntryCommentId);
        builder.HasOne(ecf => ecf.CreatedUser)
            .WithMany(e => e.EntryCommentFavorites)
            .HasForeignKey(ecf => ecf.CreatedById);
    }
}