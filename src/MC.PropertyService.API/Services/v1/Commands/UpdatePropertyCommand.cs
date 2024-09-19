using MC.PropertyService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Commands
{
    /// <summary>
    /// Command to update an existing property using MediatR.
    /// </summary>
    public class UpdatePropertyCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The ID of the property to update.
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// Property request containing all necessary data to update the property.
        /// </summary>
        public PropertyRequest Property { get; set; }

        /// <summary>
        /// New instance of the <see cref="UpdatePropertyCommand"/> class.
        /// </summary>
        /// <param name="propertyId">The ID of the property.</param>
        /// <param name="property">The details of the property to update.</param>
        public UpdatePropertyCommand(Guid propertyId, PropertyRequest property)
        {
            PropertyId = propertyId;
            Property = property;
        }
    }
}
