using MC.PropertyService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.PropertyService.API.Data.Repositories
{
    public interface IOwnerRepository
    {
        /// <summary>
        /// Retrieves an owner by its ID.
        /// </summary>
        /// <param name="ownerId">The unique identifier for the owner to retrieve.</param>
        /// <returns>The owner entity or null if no owner is found.</returns>
        Task<Owner?> GetOwnerByIdAsync(string ownerId);

        /// <summary>
        /// Adds a new owner to the database.
        /// </summary>
        /// <param name="owner">The owner entity to add.</param>
        /// <returns>The added owner entity.</returns>
        Task AddOwnerAsync(Owner owner);

        /// <summary>
        /// Updates an existing owner in the database.
        /// </summary>
        /// <param name="owner">The owner entity to update.</param>
        Task UpdateOwnerAsync(Owner owner);
    }

    public class OwnerRepository : IOwnerRepository
    {
        private readonly PropertyDBContext _context;

        public OwnerRepository(PropertyDBContext context)
        {
            _context = context;
        }

        public async Task<Owner?> GetOwnerByIdAsync(string ownerId)
        {
            return await _context.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OwnerId == ownerId);
        }

        public async Task AddOwnerAsync(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            _context.Owners.Attach(owner);
            _context.Entry(owner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
