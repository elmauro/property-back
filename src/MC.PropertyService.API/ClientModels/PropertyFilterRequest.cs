namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// This class holds the filtering criteria for querying properties in the system.
    /// </summary>
    public class PropertyFilterRequest
    {
        /// <summary>
        /// The minimum price of the property to filter.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// The maximum price of the property to filter.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// The minimum year of construction for the property.
        /// </summary>
        public int? MinYear { get; set; }

        /// <summary>
        /// The maximum year of construction for the property.
        /// </summary>
        public int? MaxYear { get; set; }

        /// <summary>
        /// The ID of the owner to filter properties owned by a specific owner.
        /// </summary>
        public string OwnerId { get; set; } = string.Empty;

        /// <summary>
        /// The name of the property to filter by.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The address of the property to filter by.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// The cadastral number of the property to filter by.
        /// </summary>
        public string CodeInternal { get; set; } = string.Empty;

        /// <summary>
        /// Allows filtering only properties created after a specific date.
        /// </summary>
        public DateTimeOffset? CreatedAfter { get; set; }

        /// <summary>
        /// Allows filtering only properties created before a specific date.
        /// </summary>
        public DateTimeOffset? CreatedBefore { get; set; }
    }
}
