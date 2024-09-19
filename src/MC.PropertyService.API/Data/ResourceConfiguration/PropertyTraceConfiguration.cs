using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.PropertyService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// Configures how property trace data is saved and relates to property.
    /// </summary>
    public class PropertyTraceConfiguration : PostgresResourceConfiguration<PropertyTrace>
    {
        public override void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            base.Configure(builder);

            // Convert PropertyTraceId to GUID in the database
            builder.Property(b => b.PropertyTraceId).HasConversion<Guid>();

            // Define relationship: A PropertyTrace belongs to one Property
            builder.HasOne(pt => pt.Property)
                   .WithMany(p => p.PropertyTraces)
                   .HasForeignKey(pt => pt.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
