using MC.PropertyService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Commands
{
    /// <summary>
    /// Command to add a new property using MediatR.
    /// </summary>
    public class AddPropertyCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// Property request containing all necessary data to create a new property.
        /// </summary>
        public PropertyRequest Property { get; set; }

        /// <summary>
        /// New instance of the <see cref="AddPropertyCommand"/> class.
        /// </summary>
        /// <param name="property">
        /// The <see cref="PropertyRequest"/> containing the details of the property to add.
        /// </param>
        public AddPropertyCommand(PropertyRequest property)
        {
            Property = property;
        }
    }
}
