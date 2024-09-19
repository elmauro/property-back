using AutoFixture;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.Tests.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the property image services.
    /// Uses AutoFixture to automatically create realistic and random data for thorough and independent tests,
    /// while handling potential circular references by omitting recursion.
    /// </summary>
    public static class PropertyImageMockingData
    {
        public static Fixture fixture;

        static PropertyImageMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new PropertyImageRequest object with random values.
        /// The 'File' property is generated with random strings simulating image file names.
        /// </summary>
        /// <returns>A new PropertyImageRequest with random values.</returns>
        public static PropertyImageRequest GetPropertyImageRequest()
        {
            return fixture.Build<PropertyImageRequest>()
                .With(pi => pi.File, fixture.Create<string>() + ".png") // Simulate a random image file name
                .With(pi => pi.Enabled, fixture.Create<int>() % 2) // Randomly assign Enabled as 0 or 1
                .Create();
        }

        /// <summary>
        /// Generates a new PropertyImage object each time it's called, with unique identifiers and timestamps.
        /// The 'PropertyImageId' is a unique GUID, and 'CreatedAt' and 'LastUpdatedAt' timestamps are set to the current time.
        /// </summary>
        /// <returns>A new PropertyImage with a unique ID and current timestamps for when it was created and last updated.</returns>
        public static PropertyImage GetPropertyImage()
        {
            return fixture.Build<PropertyImage>()
                .With(pi => pi.PropertyImageId, Guid.NewGuid().ToString())
                .With(pi => pi.PropertyId, Guid.NewGuid().ToString()) // Generate a random GUID for PropertyId
                .With(pi => pi.File, fixture.Create<string>() + ".png") // Simulate a random image file name
                .With(pi => pi.CreatedAt, DateTime.UtcNow)
                .With(pi => pi.LastUpdatedAt, DateTime.UtcNow)
                .With(pi => pi.Enabled, fixture.Create<int>() % 2) // Randomly assign Enabled as 0 or 1
                .Create();
        }
    }
}
