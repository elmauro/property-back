using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.PropertyService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// Configures how the owner data is saved and the relationship to the property.
    /// </summary>
    public class OwnerConfiguration : PostgresResourceConfiguration<Owner>
    {
        public override void Configure(EntityTypeBuilder<Owner> builder)
        {
            base.Configure(builder);

            // Convert OwnerId to GUID in the database
            builder.Property(b => b.OwnerId).HasConversion<Guid>();

            // Define relationship: An Owner has many Properties
            builder.HasMany(o => o.Properties)
                   .WithOne(p => p.Owner)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seeding Owners
            builder.HasData(
                new Owner
                {
                    OwnerId = owner1Id,
                    Name = "John Doe",
                    Address = "123 Main St, Springfield",
                    Photo = "johndoe.jpg",
                    Birthday = new DateTime(1980, 5, 20).ToUniversalTime(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Owner
                {
                    OwnerId = owner2Id,
                    Name = "Jane Smith",
                    Address = "456 Oak Ave, Springfield",
                    Photo = "janesmith.jpg",
                    Birthday = new DateTime(1985, 3, 15).ToUniversalTime(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Owner
                {
                    OwnerId = owner3Id,
                    Name = "Robert Brown",
                    Address = "789 Pine Rd, Springfield",
                    Photo = "robertbrown.jpg",
                    Birthday = new DateTime(1990, 11, 10).ToUniversalTime(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}
