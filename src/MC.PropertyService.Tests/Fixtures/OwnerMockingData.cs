using AutoFixture;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.Tests.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the owner services.
    /// Uses AutoFixture to automatically create realistic and random data for thorough and independent tests,
    /// while handling potential circular references by omitting recursion.
    /// </summary>
    public static class OwnerMockingData
    {
        public static Fixture fixture;

        static OwnerMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new OwnerRequest object with random values.
        /// </summary>
        /// <returns>A new OwnerRequest with random values.</returns>
        public static OwnerRequest GetOwnerRequest()
        {
            return fixture.Build<OwnerRequest>()
                .With(o => o.Name, fixture.Create<string>()) // Random name
                .With(o => o.Address, fixture.Create<string>()) // Random address
                .With(o => o.Photo, fixture.Create<string>() + ".jpg") // Simulate a random image file name
                .With(o => o.Birthday, fixture.Create<DateTime>().AddYears(-30).ToUniversalTime()) // Simulate a random birthday
                .Create();
        }

        /// <summary>
        /// Generates a new Owner object each time it's called, with unique identifiers and valid timestamps.
        /// </summary>
        /// <returns>A new Owner object with a unique ID and other random values.</returns>
        public static Owner GetOwner()
        {
            return fixture.Build<Owner>()
                .With(o => o.OwnerId, Guid.NewGuid().ToString()) // Random GUID for OwnerId
                .With(o => o.Name, fixture.Create<string>()) // Random name
                .With(o => o.Address, fixture.Create<string>()) // Random address
                .With(o => o.Photo, fixture.Create<string>() + ".jpg") // Simulate a random image file name
                .With(o => o.Birthday, fixture.Create<DateTime>().AddYears(-30).ToUniversalTime()) // Simulate a random birthday
                .With(o => o.CreatedAt, DateTime.UtcNow) // Current date for CreatedAt
                .With(o => o.LastUpdatedAt, DateTime.UtcNow) // Current date for LastUpdatedAt
                .Create();
        }
    }
}
