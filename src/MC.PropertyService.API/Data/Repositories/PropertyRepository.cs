using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.PropertyService.API.Data.Repositories
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// Retrieves a property by its ID.
        /// </summary>
        /// <param name="propertyId">The unique identifier for the property to retrieve.</param>
        /// <returns>The property entity or null if no property is found.</returns>
        Task<Property?> GetPropertyByIdAsync(string propertyId);

        /// <summary>
        /// Adds a new property to the database.
        /// </summary>
        /// <param name="property">The property entity to add.</param>
        /// <returns>The added property entity.</returns>
        Task AddPropertyAsync(Property property);

        /// <summary>
        /// Updates an existing property in the database.
        /// </summary>
        /// <param name="property">The property entity to update.</param>
        Task UpdatePropertyAsync(Property property);

        /// <summary>
        /// Retrieves a paginated list of properties filtered by the given criteria. and the total product count.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of properties per page.</param>
        /// <returns>
        /// A tuple containing a list of proprties for the requested page and the total count of properties.
        /// </returns>
        Task<(List<Property> Properties, int TotalCount)> ListPropertiesAsync(PropertyFilterRequest filters, int pageNumber, int pageSize);
    }

    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDBContext _context;

        public PropertyRepository(PropertyDBContext context)
        {
            _context = context;
        }

        public async Task<Property?> GetPropertyByIdAsync(string propertyId)
        {
            return await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
        }

        public async Task AddPropertyAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            _context.Properties.Attach(property);
            _context.Entry(property).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<(List<Property> Properties, int TotalCount)> ListPropertiesAsync(PropertyFilterRequest filters, int pageNumber, int pageSize)
        {
            var query = _context.Properties.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filters.Name))
            {
                query = query.Where(p => p.Name.Contains(filters.Name));
            }

            if (!string.IsNullOrEmpty(filters.Address))
            {
                query = query.Where(p => p.Address.Contains(filters.Address));
            }

            if (!string.IsNullOrEmpty(filters.CodeInternal))
            {
                query = query.Where(p => p.CodeInternal.Contains(filters.CodeInternal));
            }

            if (filters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filters.MinPrice.Value);
            }

            if (filters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);
            }

            if (filters.MinYear.HasValue)
            {
                query = query.Where(p => p.Year >= filters.MinYear.Value);
            }

            if (filters.MaxYear.HasValue)
            {
                query = query.Where(p => p.Year <= filters.MaxYear.Value);
            }

            if (!string.IsNullOrEmpty(filters.OwnerId))
            {
                query = query.Where(p => p.OwnerId == filters.OwnerId);
            }

            if (filters.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= filters.CreatedAfter.Value);
            }

            if (filters.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= filters.CreatedBefore.Value);
            }

            // Include related entities (Owner and PropertyImages)
            query = query
                .Include(p => p.Owner)
                .Include(p => p.PropertyImages);

            // Get the total count before applying pagination
            var totalCount = await query.CountAsync();

            // Apply pagination using Skip and Take
            var properties = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (properties, totalCount);
        }

    }
}
