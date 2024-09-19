using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a property that someone wants to add or change in the system.
    /// </summary>
    public class PropertyRequest
    {
        /// <see cref="Property.Name"/>
        public string Name { get; set; } = string.Empty;

        /// <see cref="Property.Address"/>
        public string Address { get; set; } = string.Empty;

        /// <see cref="Property.Price"/>
        public decimal Price { get; set; }

        /// <see cref="Property.CodeInternal"/>
        public string CodeInternal { get; set; } = string.Empty;

        /// <see cref="Property.Year"/>
        public int Year { get; set; }

        /// <see cref="Property.OwnerId"/>
        public string OwnerId { get; set; } = string.Empty;
    }
}
