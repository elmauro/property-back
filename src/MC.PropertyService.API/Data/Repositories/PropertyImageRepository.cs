using MC.PropertyService.API.Data;
using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.PropertyService.API.Data.Repositories
{
    public interface IPropertyImageRepository
    {
        /// <summary>
        /// Retrieves a property image by its ID.
        /// </summary>
        /// <param name="propertyImageId">The unique identifier for the property image to retrieve.</param>
        /// <returns>The property image entity or null if no image is found.</returns>
        Task<PropertyImage?> GetPropertyImageByIdAsync(string propertyImageId);

        /// <summary>
        /// Adds a new property image to the database.
        /// </summary>
        /// <param name="propertyImage">The property image entity to add.</param>
        /// <returns>The added property image entity.</returns>
        Task AddPropertyImageAsync(PropertyImage propertyImage);

        /// <summary>
        /// Updates an existing property image in the database.
        /// </summary>
        /// <param name="propertyImage">The property image entity to update.</param>
        Task UpdatePropertyImageAsync(PropertyImage propertyImage);
    }

    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly PropertyDBContext _context;

        public PropertyImageRepository(PropertyDBContext context)
        {
            _context = context;
        }

        public async Task<PropertyImage?> GetPropertyImageByIdAsync(string propertyImageId)
        {
            return await _context.PropertyImages
                .FirstOrDefaultAsync(pi => pi.PropertyImageId == propertyImageId);
        }

        public async Task AddPropertyImageAsync(PropertyImage propertyImage)
        {
            _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePropertyImageAsync(PropertyImage propertyImage)
        {
            _context.PropertyImages.Attach(propertyImage);
            _context.Entry(propertyImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
