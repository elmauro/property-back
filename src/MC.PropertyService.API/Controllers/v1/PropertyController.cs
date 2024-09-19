using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.PropertyService.API.Services.v1.Commands;
using MC.PropertyService.API.Options;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Services.v1.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MC.PropertyService.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="property">The property information to add.</param>
        /// <response code="201">The property was successfully created.</response>
        /// <response code="400">The property information was not valid.</response>
        /// <response code="401">Unauthprized.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<PropertyRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] PropertyRequest property)
        {
            return await _mediator.Send(new AddPropertyCommand(property));
        }

        /// <summary>
        /// Changes the price of a property.
        /// </summary>
        /// <param name="propertyId">The unique identifier of the property.</param>
        /// <param name="newPrice">The new price of the property.</param>
        /// <response code="204">The price was successfully updated.</response>
        /// <response code="400">The price information was not valid.</response>
        /// <response code="404">The property was not found.</response>
        /// <response code="401">Unauthprized.</response>
        [HttpPatch("{propertyId:guid}/price")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePrice([FromRoute] Guid propertyId, [FromBody] PriceRequest newPrice)
        {
            return await _mediator.Send(new ChangePropertyPriceCommand(propertyId, newPrice.Price));
        }

        /// <summary>
        /// Updates an existing property.
        /// </summary>
        /// <param name="propertyId">The unique identifier of the property to update.</param>
        /// <param name="property">The property information to update.</param>
        /// <response code="204">The property was successfully updated.</response>
        /// <response code="400">The property information was not valid.</response>
        /// <response code="404">The property was not found.</response>
        /// <response code="401">Unauthprized.</response>
        [HttpPut("{propertyId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] Guid propertyId, [FromBody] PropertyRequest property)
        {
            return await _mediator.Send(new UpdatePropertyCommand(propertyId, property));
        }

        /// <summary>
        /// Lists properties with optional filters.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="filters">Filter criteria to apply.</param>
        /// <response code="200">Returns the list of properties.</response>
        /// <response code="404">No properties were found.</response>
        /// <response code="401">Unauthprized.</response>
        /// <response code="401">Unauthprized.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionDataResponse<List<PropertyView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List([FromQuery] PropertyFilterRequest filters, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _mediator.Send(new ListPropertiesQuery(filters, pageNumber, pageSize));
        }
    }
}
