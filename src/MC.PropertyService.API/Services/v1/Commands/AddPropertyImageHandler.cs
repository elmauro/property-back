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
    /// Handles the addition of new property images by processing <see cref="AddPropertyImageCommand"/>.
    /// </summary>
    public class AddPropertyImageHandler : HandlerBase<AddPropertyImageCommand>
    {
        private readonly IPropertyImageRepository _repository;

        public AddPropertyImageHandler(
            IPropertyImageRepository repository,
            IMapper mapper,
            ILogger<AddPropertyImageHandler> logger) : base(mapper, logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the addition of a property image to the database.
        /// </summary>
        /// <param name="request">The command containing the image data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add property image operation.</returns>
        public override async Task<IActionResult> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newPropertyImage = _mapper.Map<PropertyImage>(request.PropertyImage);
                newPropertyImage.CreatedBy = systemUser;
                newPropertyImage.LastUpdatedBy = systemUser;
                newPropertyImage.PropertyId = request.PropertyId.ToString();

                await _repository.AddPropertyImageAsync(newPropertyImage);

                var response = new ActionDataResponse<PropertyImageRequest>(request.PropertyImage);
                return new CreatedAtActionResult(nameof(PropertyImageController.AddImage), null, new { propertyImageId = newPropertyImage.PropertyImageId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a property image");
                return GetErrorObjectResult();
            }
        }
    }
}
