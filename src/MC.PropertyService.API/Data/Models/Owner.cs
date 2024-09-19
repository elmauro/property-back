using MC.PropertyService.API.Data.ResourceConfiguration;

namespace MC.PropertyService.API.Data.Models
{
    /// <summary>
    /// This class represents an owner in the system. It allows the owner to be saved and found in the database.
    /// </summary>
    public class Owner : IResource
    {
        /// <summary>
        /// Unique identifier for the owner. It is created automatically.
        /// </summary>
        public string OwnerId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The name of the owner.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The address of the owner.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// The photo of the owner (could be a URL or base64 encoded string).
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// The birthday of the owner.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Navigation property for the properties owned by this owner.
        /// </summary>
        public ICollection<Property> Properties { get; set; } = new List<Property>();

        /// <summary>
        /// The identification of the person who first added the owner to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the owner details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the owner was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the owner details were changed.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
