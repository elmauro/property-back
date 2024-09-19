using AutoMapper;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Controllers.v1;
using MC.PropertyService.API.Data.Models;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Options;
using MC.PropertyService.API.Services.v1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Handlers
{
    /// <summary>
    /// Handles the addition of new properties by processing <see cref="AddPropertyCommand"/>.
    /// </summary>
    public class AddPropertyHandler : HandlerBase<AddPropertyCommand>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;

        public AddPropertyHandler(
            IPropertyRepository propertyRepository,
            IOwnerRepository ownerRepository,
            IMapper mapper,
            ILogger<AddPropertyHandler> logger) : base(mapper, logger)
        {
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
        }

        /// <summary>
        /// Handles the addition of a property to the database.
        /// </summary>
        /// <param name="request">The command containing the property data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add property operation.</returns>
        public override async Task<IActionResult> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing Owner from the repository
                var existingOwner = await _ownerRepository.GetOwnerByIdAsync(request.Property.OwnerId);

                if (existingOwner == null)
                    return new NotFoundResult();

                var newProperty = _mapper.Map<Property>(request.Property);
                newProperty.CreatedBy = systemUser;
                newProperty.LastUpdatedBy = systemUser;

                await _propertyRepository.AddPropertyAsync(newProperty);

                var response = new ActionDataResponse<PropertyRequest>(request.Property);
                return new CreatedAtActionResult(nameof(PropertyController.Create), null, new { propertyId = newProperty.PropertyId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new property");
                return GetErrorObjectResult();
            }
        }
    }
}
