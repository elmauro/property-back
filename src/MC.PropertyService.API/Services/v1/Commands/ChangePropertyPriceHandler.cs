using AutoMapper;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Services.v1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Handlers
{
    /// <summary>
    /// Handles changing the price of a property by processing <see cref="ChangePropertyPriceCommand"/>.
    /// </summary>
    public class ChangePropertyPriceHandler : HandlerBase<ChangePropertyPriceCommand>
    {
        private readonly IPropertyRepository _repository;

        public ChangePropertyPriceHandler(
            IPropertyRepository repository,
            IMapper mapper,
            ILogger<ChangePropertyPriceHandler> logger) : base(mapper, logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the price change of a property in the database.
        /// </summary>
        /// <param name="request">The command containing the property price change data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the change property price operation.</returns>
        public override async Task<IActionResult> Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _repository.GetPropertyByIdAsync(request.PropertyId.ToString());
                if (property == null)
                {
                    return new NotFoundResult();
                }

                property.Price = request.NewPrice;

                // Update the 'LastUpdatedBy' and 'LastUpdatedAt' properties to reflect the current system user and the current UTC time.
                property.LastUpdatedBy = systemUser;
                property.LastUpdatedAt = DateTime.UtcNow;

                await _repository.UpdatePropertyAsync(property);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing property price");
                return GetErrorObjectResult();
            }
        }
    }
}
