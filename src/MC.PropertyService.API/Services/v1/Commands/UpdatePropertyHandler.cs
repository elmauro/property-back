using AutoMapper;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Services.v1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Handlers
{
    /// <summary>
    /// Handles updating a property by processing <see cref="UpdatePropertyCommand"/>.
    /// </summary>
    public class UpdatePropertyHandler : HandlerBase<UpdatePropertyCommand>
    {
        private readonly IPropertyRepository _repository;
        private readonly IOwnerRepository _ownerRepository;

        public UpdatePropertyHandler(
            IPropertyRepository repository,
            IOwnerRepository ownerRepository,
            IMapper mapper,
            ILogger<UpdatePropertyHandler> logger) : base(mapper, logger)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
        }

        /// <summary>
        /// Handles updating a property in the database.
        /// </summary>
        /// <param name="request">The command containing the property update data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the update property operation.</returns>
        public override async Task<IActionResult> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingOwner = await _ownerRepository.GetOwnerByIdAsync(request.Property.OwnerId);

                if (existingOwner == null)
                    return new NotFoundResult();

                var property = await _repository.GetPropertyByIdAsync(request.PropertyId.ToString());
                if (property == null)
                {
                    return new NotFoundResult();
                }

                _mapper.Map(request.Property, property);

                property.LastUpdatedBy = systemUser;
                property.LastUpdatedBy = systemUser;

                await _repository.UpdatePropertyAsync(property);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a property");
                return GetErrorObjectResult();
            }
        }
    }
}
