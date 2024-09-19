using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MC.PropertyService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// This is used to keep track of who created or changed a record and when it happened.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Who made this record? Could be a user's name or an app's name.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Who last changed this record?
        /// </summary>
        string LastUpdatedBy { get; set; }

        /// <summary>
        /// When was this record first made?
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When was this record last changed?
        /// </summary>
        DateTimeOffset LastUpdatedAt { get; set; }
    }

    /// <summary>
    /// This sets up the basic rules for saving records in the database that track creation and changes.
    /// </summary>
    /// <typeparam name="T">The type of the record. It needs to be a class that can be created without any arguments.</typeparam>
    public class CommonResourceConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IResource, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Makes sure there's a quick way to look up when records were created.
            builder.HasIndex((T resource) => resource.CreatedAt);
        }
    }

    /// <summary>
    /// Sets up specific rules for PostgreSQL to manage when records were created or changed.
    /// </summary>
    /// <typeparam name="T">The type of the record.</typeparam>
    public class PostgresResourceConfiguration<T> : CommonResourceConfiguration<T> where T : class, IResource, new()
    {
        // Define specific GUIDs for consistent references
        public const string owner1Id = "00000000-0000-0000-0000-000000000001";
        public const string owner2Id = "00000000-0000-0000-0000-000000000002";
        public const string owner3Id = "00000000-0000-0000-0000-000000000003";
        public const string owner4Id = "00000000-0000-0000-0000-000000000004";
        public const string owner5Id = "00000000-0000-0000-0000-000000000005";

        // Defining Property IDs
        public const string property1Id = "00000000-0000-0000-0000-000000000101";
        public const string property2Id = "00000000-0000-0000-0000-000000000102";
        public const string property3Id = "00000000-0000-0000-0000-000000000103";
        public const string property4Id = "00000000-0000-0000-0000-000000000104";
        public const string property5Id = "00000000-0000-0000-0000-000000000105";
        public const string property6Id = "00000000-0000-0000-0000-000000000106";
        public const string property7Id = "00000000-0000-0000-0000-000000000107";
        public const string property8Id = "00000000-0000-0000-0000-000000000108";
        public const string property9Id = "00000000-0000-0000-0000-000000000109";
        public const string property10Id = "00000000-0000-0000-0000-000000000110";
        public const string property11Id = "00000000-0000-0000-0000-000000000111";
        public const string property12Id = "00000000-0000-0000-0000-000000000112";
        public const string property13Id = "00000000-0000-0000-0000-000000000113";
        public const string property14Id = "00000000-0000-0000-0000-000000000114";
        public const string property15Id = "00000000-0000-0000-0000-000000000115";
        public const string property16Id = "00000000-0000-0000-0000-000000000116";

        public const string propertyImage1Id = "00000000-0000-0000-0000-000000000101";
        public const string propertyImage2Id = "00000000-0000-0000-0000-000000000102";
        public const string propertyImage3Id = "00000000-0000-0000-0000-000000000103";
        public const string propertyImage4Id = "00000000-0000-0000-0000-000000000104";
        public const string propertyImage5Id = "00000000-0000-0000-0000-000000000105";
        public const string propertyImage6Id = "00000000-0000-0000-0000-000000000106";
        public const string propertyImage7Id = "00000000-0000-0000-0000-000000000107";
        public const string propertyImage8Id = "00000000-0000-0000-0000-000000000108";
        public const string propertyImage9Id = "00000000-0000-0000-0000-000000000109";
        public const string propertyImage10Id = "00000000-0000-0000-0000-000000000110";
        public const string propertyImage11Id = "00000000-0000-0000-0000-000000000111";
        public const string propertyImage12Id = "00000000-0000-0000-0000-000000000112";
        public const string propertyImage13Id = "00000000-0000-0000-0000-000000000113";
        public const string propertyImage14Id = "00000000-0000-0000-0000-000000000114";
        public const string propertyImage15Id = "00000000-0000-0000-0000-000000000115";
        public const string propertyImage16Id = "00000000-0000-0000-0000-000000000116";


        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            // Sets up the date and time types for PostgreSQL and makes sure new records get the current time.
            builder.Property((T e) => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now() at time zone 'utc'");
            builder.Property((T e) => e.LastUpdatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now() at time zone 'utc'");
        }
    }
}
