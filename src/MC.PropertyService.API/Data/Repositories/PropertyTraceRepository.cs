using MC.PropertyService.API.Data;
using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.PropertyService.API.Data.Repositories
{
    public interface IPropertyTraceRepository
    {
        /// <summary>
        /// Retrieves a property trace by its ID.
        /// </summary>
        /// <param name="propertyTraceId">The unique identifier for the property trace to retrieve.</param>
        /// <returns>The property trace entity or null if no trace is found.</returns>
        Task<PropertyTrace?> GetPropertyTraceByIdAsync(string propertyTraceId);

        /// <summary>
        /// Adds a new property trace to the database.
        /// </summary>
        /// <param name="propertyTrace">The property trace entity to add.</param>
        /// <returns>The added property trace entity.</returns>
        Task AddPropertyTraceAsync(PropertyTrace propertyTrace);

        /// <summary>
        /// Updates an existing property trace in the database.
        /// </summary>
        /// <param name="propertyTrace">The property trace entity to update.</param>
        Task UpdatePropertyTraceAsync(PropertyTrace propertyTrace);
    }

    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly PropertyDBContext _context;

        public PropertyTraceRepository(PropertyDBContext context)
        {
            _context = context;
        }

        public async Task<PropertyTrace?> GetPropertyTraceByIdAsync(string propertyTraceId)
        {
            return await _context.PropertyTraces
                .FirstOrDefaultAsync(pt => pt.PropertyTraceId == propertyTraceId);
        }

        public async Task AddPropertyTraceAsync(PropertyTrace propertyTrace)
        {
            _context.PropertyTraces.Add(propertyTrace);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePropertyTraceAsync(PropertyTrace propertyTrace)
        {
            _context.PropertyTraces.Attach(propertyTrace);
            _context.Entry(propertyTrace).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
