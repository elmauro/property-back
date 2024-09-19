using MC.PropertyService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.PropertyService.API.Services.v1.Queries
{
    /// <summary>
    /// Query to list properties with filters using MediatR.
    /// </summary>
    public class ListPropertiesQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// The page number to retrieve.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of properties per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Filter request containing the filtering criteria for listing properties.
        /// </summary>
        public PropertyFilterRequest Filters { get; set; }

        /// <summary>
        /// New instance of the <see cref="ListPropertiesQuery"/> class.
        /// </summary>
        /// <param name="filters">The filtering criteria for listing properties.</param>
        public ListPropertiesQuery(PropertyFilterRequest filters, int pageNumber, int pageSize)
        {
            Filters = filters;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
