using MC.PropertyService.API.Data.ResourceConfiguration;

namespace MC.PropertyService.API.Data.Models
{
    /// <summary>
    /// This class represents a trace or historical record for a property.
    /// </summary>
    public class PropertyTrace : IResource
    {
        /// <summary>
        /// Unique identifier for the property trace. It is created automatically.
        /// </summary>
        public string PropertyTraceId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The date when the sale or event related to this trace occurred.
        /// </summary>
        public DateTime DateSale { get; set; }

        /// <summary>
        /// The name of the owner or entity at the time of the sale.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The value of the property at the time of the trace.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// The tax value associated with the property at the time of the trace.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Foreign key for the property.
        /// </summary>
        public string PropertyId { get; set; }

        /// <summary>
        /// Navigation property for the property related to this trace.
        /// </summary>
        public Property Property { get; set; }

        /// <summary>
        /// The identification of the person who first added the trace to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the trace details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the trace was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the trace details were changed.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
