using MC.PropertyService.API.Data.ResourceConfiguration;

namespace MC.PropertyService.API.Data.Models
{
    /// <summary>
    /// This class represents a property in the system. It allows the property to be saved and found in the database.
    /// </summary>
    public class Property : IResource
    {
        /// <summary>
        /// Unique identifier for the property. It is created automatically.
        /// </summary>
        public string PropertyId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The name given to the property.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The address of the property.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// The price of the property.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The cadastral number of the property.
        /// </summary>
        public string CodeInternal { get; set; } = string.Empty;

        /// <summary>
        /// The year the property was built.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Foreign key for the owner.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Navigation property for the owner of this property.
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// Navigation property for the images related to this property.
        /// </summary>
        public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

        /// <summary>
        /// Navigation property for the trace records related to this property.
        /// </summary>
        public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();

        /// <summary>
        /// The identification of the person who first added the property to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the property details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the property was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the property details were changed.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
