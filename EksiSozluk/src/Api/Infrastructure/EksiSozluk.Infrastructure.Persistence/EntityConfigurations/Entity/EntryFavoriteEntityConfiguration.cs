using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.Entity;

public class EntryFavoriteEntityConfiguration : BaseEntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);
        builder.ToTable("entryfavorite", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(ef => ef.Entry)
            .WithMany(e => e.EntryFavorites)
            .HasForeignKey(ef => ef.EntryId);
        builder.HasOne(ef => ef.CreatedUser)
            .WithMany(u => u.EntryFavorites)
            .HasForeignKey(ef => ef.CreatedById);
    }
}