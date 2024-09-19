using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.PropertyService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// Configures how property image data is saved and relates to property.
    /// </summary>
    public class PropertyImageConfiguration : PostgresResourceConfiguration<PropertyImage>
    {
        public override void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            base.Configure(builder);

            // Convert PropertyImageId to GUID in the database
            builder.Property(b => b.PropertyImageId).HasConversion<Guid>();

            // Define relationship: A PropertyImage belongs to one Property
            builder.HasOne(pi => pi.Property)
                   .WithMany(p => p.PropertyImages)
                   .HasForeignKey(pi => pi.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seeding Property Images
            builder.HasData(
                new PropertyImage
                {
                    PropertyImageId = propertyImage1Id,
                    PropertyId = property1Id,
                    File = "green_acres_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage2Id,
                    PropertyId = property2Id,
                    File = "ocean_view_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage3Id,
                    PropertyId = property3Id,
                    File = "mountain_retreat_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage4Id,
                    PropertyId = property4Id,
                    File = "city_lights_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage5Id,
                    PropertyId = property5Id,
                    File = "suburban_dream_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage6Id,
                    PropertyId = property6Id,
                    File = "downtown_loft_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage7Id,
                    PropertyId = property7Id,
                    File = "countryside_estate_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage8Id,
                    PropertyId = property8Id,
                    File = "lakehouse_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage9Id,
                    PropertyId = property9Id,
                    File = "penthouse_suite_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage10Id,
                    PropertyId = property10Id,
                    File = "country_cottage_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage11Id,
                    PropertyId = property11Id,
                    File = "luxury_villa_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage12Id,
                    PropertyId = property12Id,
                    File = "bungalow_bliss_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage13Id,
                    PropertyId = property13Id,
                    File = "forest_lodge_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage14Id,
                    PropertyId = property14Id,
                    File = "seaside_cottage_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage15Id,
                    PropertyId = property15Id,
                    File = "urban_studio_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new PropertyImage
                {
                    PropertyImageId = propertyImage16Id,
                    PropertyId = property16Id,
                    File = "hillside_manor_1.jpg",
                    Enabled = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}
