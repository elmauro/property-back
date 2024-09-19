using Microsoft.EntityFrameworkCore;
using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.Data
{
    /// <summary>
    /// Provides context for property database interactions, encapsulating configuration
    /// and functionality for accessing the database through Entity Framework Core.
    /// </summary>
    /// <remarks>
    /// This context is configured to use specific database options provided during instantiation
    /// and applies entity configurations dynamically from all configurations defined in the assembly.
    /// </remarks>
    public class PropertyDBContext(DbContextOptions<PropertyDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDBContext"/> class.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasAnnotation("Relational:Collation", "en_US.utf8")
                .ApplyConfigurationsFromAssembly(typeof(PropertyDBContext).Assembly);
        }
    }
}