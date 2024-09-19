using AutoMapper;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Options;
using MC.PropertyService.API.Services.v1.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Handlers
{
    /// <summary>
    /// Handles listing properties with filters by processing <see cref="ListPropertiesQuery"/>.
    /// </summary>
    public class ListPropertiesHandler : HandlerBase<ListPropertiesQuery>
    {
        private readonly IPropertyRepository _repository;

        public ListPropertiesHandler(
            IPropertyRepository repository,
            IMapper mapper,
            ILogger<ListPropertiesHandler> logger) : base(mapper, logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles listing properties with filters from the database.
        /// </summary>
        /// <param name="request">The query containing the filter data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the list properties operation.</returns>
        public override async Task<IActionResult> Handle(ListPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var (properties, totalCount) = await _repository.ListPropertiesAsync(request.Filters, request.PageNumber, request.PageSize);

                if (properties == null || !properties.Any())
                    return new NotFoundResult();

                // Map to PropertyView
                var propertyViews = properties.Select(PropertyView.Project().Compile()).ToList();

                // Construct the paginated response
                var response = new ActionDataResponseList<List<PropertyView>>(propertyViews, totalCount, request.PageNumber, request.PageSize);

                // Return the result
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while listing properties");
                return GetErrorObjectResult();
            }
        }
    }
}
