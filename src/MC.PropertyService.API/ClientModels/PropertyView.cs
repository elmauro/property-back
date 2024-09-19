using MC.PropertyService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// Projects a <see cref="Property"/> entity to a <see cref="PropertyView"/> model.
    /// </summary>
    /// <returns>
    /// Helps to convert a Property's data into a PropertyView format.
    /// </returns>
    public class PropertyView
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

        /// <see cref="Property.PropertyId"/>
        public string PropertyId { get; set; } = string.Empty;

        /// <see cref="Property.OwnerId"/>
        public string OwnerId { get; set; } = string.Empty;

        /// <see cref="Property.Owner"/>
        public OwnerView? Owner { get; set; }

        /// <see cref="Property.PropertyImages"/>
        public ICollection<PropertyImageView>? PropertyImages { get; set; }

        /// <see cref="Property.CreatedBy"/>
        public string CreatedBy { get; set; } = string.Empty;

        /// <see cref="Property.LastUpdatedBy"/>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <see cref="Property.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <see cref="Property.LastUpdatedAt"/>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Provides a way to automatically create a PropertyView from a Property.
        /// </summary>
        public static Expression<Func<Property, PropertyView>> Project() => property => new PropertyView
        {
            PropertyId = property.PropertyId,
            OwnerId = property.OwnerId,

            // Use the OwnerView projection for the Owner
            Owner = property.Owner != null ? new OwnerView
            {
                Name = property.Owner.Name,
                Address = property.Owner.Address,
                Photo = property.Owner.Photo
            } : null,

            // Use the PropertyImageView projection for PropertyImages
            PropertyImages = property.PropertyImages.Select(pi => new PropertyImageView
            {
                File = pi.File,
                Enabled = pi.Enabled
            }).ToList(),

            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            CreatedBy = property.CreatedBy,
            LastUpdatedBy = property.LastUpdatedBy,
            CreatedAt = property.CreatedAt,
            LastUpdatedAt = property.LastUpdatedAt
        };
    }
}
