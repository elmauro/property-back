using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Commands
{
    /// <summary>
    /// Command to change the price of a property using MediatR.
    /// </summary>
    public class ChangePropertyPriceCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The ID of the property to update the price for.
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// The new price for the property.
        /// </summary>
        public decimal NewPrice { get; set; }

        /// <summary>
        /// New instance of the <see cref="ChangePropertyPriceCommand"/> class.
        /// </summary>
        /// <param name="propertyId">The ID of the property.</param>
        /// <param name="newPrice">The new price for the property.</param>
        public ChangePropertyPriceCommand(Guid propertyId, decimal newPrice)
        {
            PropertyId = propertyId;
            NewPrice = newPrice;
        }
    }
}
