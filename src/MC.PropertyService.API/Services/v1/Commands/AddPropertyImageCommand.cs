using MC.PropertyService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Commands
{
    /// <summary>
    /// Command to add a new image to a property using MediatR.
    /// </summary>
    public class AddPropertyImageCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The ID of the property to which the image is added.
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// Property image request containing all necessary data to add an image.
        /// </summary>
        public PropertyImageRequest PropertyImage { get; set; }

        /// <summary>
        /// New instance of the <see cref="AddPropertyImageCommand"/> class.
        /// </summary>
        /// <param name="propertyId">The ID of the property.</param>
        /// <param name="propertyImage">The details of the property image.</param>
        public AddPropertyImageCommand(Guid propertyId, PropertyImageRequest propertyImage)
        {
            PropertyId = propertyId;
            PropertyImage = propertyImage;
        }
    }
}
