using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.PropertyService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// Configures how property data is saved and the relationships to owner, property images, and property traces.
    /// </summary>
    public class PropertyConfiguration : PostgresResourceConfiguration<Property>
    {
        public override void Configure(EntityTypeBuilder<Property> builder)
        {
            base.Configure(builder);

            // Convert PropertyId to GUID in the database
            builder.Property(b => b.PropertyId).HasConversion<Guid>();

            // Define relationship: A Property belongs to one Owner
            builder.HasOne(p => p.Owner)
                   .WithMany(o => o.Properties)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Define relationship: A Property has many PropertyImages
            builder.HasMany(p => p.PropertyImages)
                   .WithOne(pi => pi.Property)
                   .HasForeignKey(pi => pi.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Define relationship: A Property has many PropertyTraces
            builder.HasMany(p => p.PropertyTraces)
                   .WithOne(pt => pt.Property)
                   .HasForeignKey(pt => pt.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seeding Properties
            builder.HasData(
                new Property
                {
                    PropertyId = property1Id,
                    Name = "Green Acres",
                    Address = "123 Country Road, Springfield",
                    Price = 250000M,
                    CodeInternal = "GR12345",
                    Year = 2010,
                    OwnerId = owner1Id, // Assuming this owner already exists
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property2Id,
                    Name = "Ocean View",
                    Address = "456 Beach Blvd, Springfield",
                    Price = 450000M,
                    CodeInternal = "OV45678",
                    Year = 2015,
                    OwnerId = owner2Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property3Id,
                    Name = "Mountain Retreat",
                    Address = "789 Hilltop Dr, Springfield",
                    Price = 350000M,
                    CodeInternal = "MR78910",
                    Year = 2012,
                    OwnerId = owner3Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property4Id,
                    Name = "City Lights",
                    Address = "101 Downtown Ave, Springfield",
                    Price = 600000M,
                    CodeInternal = "CL10111",
                    Year = 2020,
                    OwnerId = owner1Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property5Id,
                    Name = "Suburban Dream",
                    Address = "202 Suburb Ln, Springfield",
                    Price = 300000M,
                    CodeInternal = "SD20222",
                    Year = 2005,
                    OwnerId = owner2Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property6Id,
                    Name = "Downtown Loft",
                    Address = "303 City Center Rd, Springfield",
                    Price = 450000M,
                    CodeInternal = "DL30333",
                    Year = 2018,
                    OwnerId = owner3Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property7Id,
                    Name = "Countryside Estate",
                    Address = "404 Rural Dr, Springfield",
                    Price = 700000M,
                    CodeInternal = "CE40444",
                    Year = 2017,
                    OwnerId = owner1Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property8Id,
                    Name = "Lakehouse",
                    Address = "505 Lakeside Rd, Springfield",
                    Price = 800000M,
                    CodeInternal = "LH50555",
                    Year = 2019,
                    OwnerId = owner2Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property9Id,
                    Name = "Penthouse Suite",
                    Address = "606 High Rise Blvd, Springfield",
                    Price = 950000M,
                    CodeInternal = "PH60666",
                    Year = 2021,
                    OwnerId = owner3Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property10Id,
                    Name = "Country Cottage",
                    Address = "707 Farm Ln, Springfield",
                    Price = 200000M,
                    CodeInternal = "CC70777",
                    Year = 2000,
                    OwnerId = owner1Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property11Id,
                    Name = "Luxury Villa",
                    Address = "808 Palm Ave, Springfield",
                    Price = 1200000M,
                    CodeInternal = "LV80888",
                    Year = 2022,
                    OwnerId = owner2Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property12Id,
                    Name = "Bungalow Bliss",
                    Address = "909 River Rd, Springfield",
                    Price = 275000M,
                    CodeInternal = "BB90999",
                    Year = 2008,
                    OwnerId = owner3Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property13Id,
                    Name = "Forest Lodge",
                    Address = "1001 Woods Ln, Springfield",
                    Price = 650000M,
                    CodeInternal = "FL101010",
                    Year = 2016,
                    OwnerId = owner1Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property14Id,
                    Name = "Seaside Cottage",
                    Address = "1102 Coastal Rd, Springfield",
                    Price = 300000M,
                    CodeInternal = "SC111011",
                    Year = 2011,
                    OwnerId = owner2Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property15Id,
                    Name = "Urban Studio",
                    Address = "1203 Metro Blvd, Springfield",
                    Price = 350000M,
                    CodeInternal = "US121212",
                    Year = 2014,
                    OwnerId = owner3Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                },
                new Property
                {
                    PropertyId = property16Id,
                    Name = "Hillside Manor",
                    Address = "1304 Valley Rd, Springfield",
                    Price = 500000M,
                    CodeInternal = "HM131313",
                    Year = 2023,
                    OwnerId = owner1Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                }
            );

        }
    }
}
