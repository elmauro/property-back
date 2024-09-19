using MC.PropertyService.API.Data.ResourceConfiguration;

namespace MC.PropertyService.API.Data.Models
{
    /// <summary>
    /// This class represents an image associated with a property in the system.
    /// </summary>
    public class PropertyImage : IResource
    {
        /// <summary>
        /// Unique identifier for the property image. It is created automatically.
        /// </summary>
        public string PropertyImageId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Foreign key for the property.
        /// </summary>
        public string PropertyId { get; set; }

        /// <summary>
        /// Navigation property for the property that owns this image.
        /// </summary>
        public Property Property { get; set; }

        /// <summary>
        /// The file path or URL of the property image.
        /// </summary>
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the image is enabled (1) or not (0).
        /// </summary>
        public int Enabled { get; set; } = 1;

        /// <summary>
        /// The identification of the person who first added the image to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the image details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the image was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the image details were changed.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
