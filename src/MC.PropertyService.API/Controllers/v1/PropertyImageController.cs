using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Services.v1.Commands;
using MC.PropertyService.API.Options;
using Microsoft.AspNetCore.Authorization;

namespace MC.PropertyService.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("v1/properties/{propertyId:guid}/images")]
    [Produces("application/json")]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets");

        public PropertyImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds an image to an existing property and saves it to the assets folder.
        /// </summary>
        /// <param name="propertyId">The unique identifier of the property.</param>
        /// <param name="file">The image file to upload.</param>
        /// <response code="201">The image was successfully added and saved.</response>
        /// <response code="400">The image file was not valid.</response>
        /// <response code="401">Unauthprized.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddImage(Guid propertyId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ValidationErrorResponse());
            }

            try
            {
                // Ensure the assets directory exists
                if (!Directory.Exists(_uploadPath))
                {
                    Directory.CreateDirectory(_uploadPath);
                }

                // Build the file path with the original file name
                var filePath = Path.Combine(_uploadPath, file.FileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Assuming you save the file information in the database through MediatR
                var propertyImageRequest = new PropertyImageRequest
                {
                    File = file.FileName, // Storing the file name or relative path in the database
                    Enabled = 1 // Assuming this field is used to enable/disable images
                };

                // Send command to save the image in the property (this is optional if needed)
                return await _mediator.Send(new AddPropertyImageCommand(propertyId, propertyImageRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
