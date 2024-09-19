using AutoFixture;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.Tests.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the property services.
    /// Uses AutoFixture to automatically create realistic and random data for thorough and independent tests.
    /// </summary>
    public static class PropertyMockingData
    {
        public static Fixture fixture = new Fixture();

        static PropertyMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new PropertyRequest object with random values.
        /// The 'Price' property is randomly generated, while other properties such as Name, Address, etc., are filled with random data.
        /// </summary>
        /// <returns>A new PropertyRequest with random values.</returns>
        public static PropertyRequest GetPropertyRequest()
        {
            return fixture.Build<PropertyRequest>()
                .With(pi => pi.OwnerId, Guid.NewGuid().ToString()) // Generate a random GUID for OwnerId
                .With(pr => pr.Price, fixture.Create<decimal>())
                .With(pr => pr.Year, fixture.Create<int>() % 100 + 1920) // Random year between 1920 and present
                .Create();
        }

        /// <summary>
        /// Generates a new Property object each time it's called, with unique identifiers and timestamps.
        /// The 'PropertyId' is a unique GUID, and the 'CreatedAt' and 'LastUpdatedAt' timestamps are set to the current time.
        /// </summary>
        /// <returns>A new Property with a unique ID and current timestamps for when it was created and last updated.</returns>
        public static Property GetProperty()
        {
            return fixture.Build<Property>()
                .With(p => p.PropertyId, Guid.NewGuid().ToString())
                .With(pi => pi.OwnerId, Guid.NewGuid().ToString())
                .With(p => p.CreatedAt, DateTime.UtcNow)
                .With(p => p.LastUpdatedAt, DateTime.UtcNow)
                .With(p => p.Price, fixture.Create<decimal>())
                .With(p => p.Year, fixture.Create<int>() % 100 + 1920) // Random year between 1920 and present
                .Create();
        }
    }
}
