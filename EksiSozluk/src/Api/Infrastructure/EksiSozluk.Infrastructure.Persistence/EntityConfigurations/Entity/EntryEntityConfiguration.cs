using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EksiSozluk.Infrastructure.Persistence.EntityConfigurations.Entity;

public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);
        builder.ToTable("entry", EksiSozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(e => e.CreatedBy)
            .WithMany(u => u.Entries)
            .HasForeignKey(e => e.CreatedById);
    }
}