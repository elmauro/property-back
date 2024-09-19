using FluentAssertions;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data;
using MC.PropertyService.API.Options;
using MC.PropertyService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MC.PropertyService.Tests.Integration.v1
{
    public class PropertyControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly string _token;
        private const string PropertyRoute = "v1/Property";

        public PropertyControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _token = AuthenticationMockingData.GenerateJwtToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async Task AddPropertyReturnsOk()
        {
            // Arrange
            var propertyToAdd = PropertyMockingData.GetPropertyRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PropertyDBContext>();

            var ownerToAdd = OwnerMockingData.GetOwner();
            ownerToAdd.Properties = [];
            dbContext.Owners.Add(ownerToAdd);
            await dbContext.SaveChangesAsync();

            propertyToAdd.OwnerId = ownerToAdd.OwnerId;

            await dbContext.SaveChangesAsync();

            // Act
            var response = await _client.PostAsJsonAsync($"{PropertyRoute}", propertyToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var propertyId = query.Get("propertyId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<PropertyRequest>>();
            actionDataResponse.Should().NotBeNull();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.Name.Should().Be(propertyToAdd.Name);
            actionDataResponse?.Data.Address.Should().Be(propertyToAdd.Address);
            actionDataResponse?.Data.Price.Should().Be(propertyToAdd.Price);
            actionDataResponse?.Data.CodeInternal.Should().Be(propertyToAdd.CodeInternal);
            actionDataResponse?.Data.Year.Should().Be(propertyToAdd.Year);

            var propertyToRemove = await dbContext.Properties.FindAsync(propertyId);
            dbContext.Properties.Remove(propertyToRemove);
            dbContext.Owners.Remove(ownerToAdd);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task CreateProperty_ReturnsBadRequest()
        {
            // Arrange
            var propertyRequest = new PropertyRequest { Name = string.Empty }; // Invalid property (Name is empty)

            // Act
            var response = await _client.PostAsJsonAsync($"{PropertyRoute}", propertyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ChangePropertyPrice_ReturnsNoContent()
        {
            // Arrange
            var propertyToUpdate = PropertyMockingData.GetPropertyRequest();

            // Use the factory to create a scope and resolve the PropertyDBContext
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PropertyDBContext>();

            var ownerToAdd = OwnerMockingData.GetOwner();
            ownerToAdd.Properties = [];
            dbContext.Owners.Add(ownerToAdd);
            await dbContext.SaveChangesAsync();

            propertyToUpdate.OwnerId = ownerToAdd.OwnerId;

            // Act: First, create a property to be updated
            var response = await _client.PostAsJsonAsync($"{PropertyRoute}", propertyToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var propertyId = query.Get("propertyId");

            // Act
            var priceRequest = new PriceRequest
            {
                Price = 120000M
            };
            var updateResponse = await _client.PatchAsJsonAsync($"{PropertyRoute}/{propertyId}/price", priceRequest);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingProperty = await dbContext.Properties.FindAsync(propertyId);

            existingProperty?.Price.Should().Be(priceRequest.Price);

            // Clean up
            dbContext.Owners.Remove(ownerToAdd);
            await dbContext.SaveChangesAsync();
        }


        [Fact]
        public async Task ChangePropertyPrice_ReturnsNotFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid().ToString();
            var priceRequest = new PriceRequest
            {
                Price = 120000M
            };

            // Act
            var response = await _client.PatchAsJsonAsync($"{PropertyRoute}/{propertyId}/price", priceRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task UpdateProperty_ReturnsNoContent()
        {
            // Arrange
            var propertyToUpdate = PropertyMockingData.GetPropertyRequest();

            // Use the factory to create a scope and resolve the PropertyDBContext
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PropertyDBContext>();

            var ownerToAdd = OwnerMockingData.GetOwner();
            ownerToAdd.Properties = [];
            dbContext.Owners.Add(ownerToAdd);
            await dbContext.SaveChangesAsync();

            propertyToUpdate.OwnerId = ownerToAdd.OwnerId;

            // Act: First, create a property to be updated
            var response = await _client.PostAsJsonAsync($"{PropertyRoute}", propertyToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var propertyId = query.Get("propertyId");

            var updateResponse = await _client.PutAsJsonAsync($"{PropertyRoute}/{propertyId}", propertyToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingProperty = await dbContext.Properties.FindAsync(propertyId);

            // Verify that the property was updated correctly
            existingProperty?.PropertyId.Should().Be(propertyId);
            existingProperty?.Name.Should().Be(propertyToUpdate.Name);
            existingProperty?.Address.Should().Be(propertyToUpdate.Address);
            existingProperty?.Price.Should().Be(propertyToUpdate.Price);
            existingProperty?.CodeInternal.Should().Be(propertyToUpdate.CodeInternal);
            existingProperty?.Year.Should().Be(propertyToUpdate.Year);

            // Clean up by removing the updated property
            dbContext.Owners.Remove(ownerToAdd);
            dbContext.Properties.Remove(existingProperty);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateProperty_ReturnsNotFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid().ToString();
            var propertyRequest = PropertyMockingData.GetPropertyRequest();

            // Act
            var response = await _client.PutAsJsonAsync($"{PropertyRoute}/{propertyId}", propertyRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ListPropertiesWithFiltersReturnsOk()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PropertyDBContext>();

            var ownerToAdd = OwnerMockingData.GetOwner();
            ownerToAdd.Properties = [];
            dbContext.Owners.Add(ownerToAdd);
            await dbContext.SaveChangesAsync();

            // Adding properties to the database for testing filters
            var property1 = PropertyMockingData.GetProperty();
            property1.Price = 60000M;  // Price within the filter range
            property1.OwnerId = ownerToAdd.OwnerId;
            property1.Owner = ownerToAdd;
            property1.PropertyImages = [];
            property1.PropertyTraces = [];
            dbContext.Properties.Add(property1);

            var property2 = PropertyMockingData.GetProperty();
            property2.Price = 150000M;  // Price within the filter range
            property2.OwnerId = ownerToAdd.OwnerId;
            property2.Owner = ownerToAdd;
            property2.PropertyImages = [];
            property2.PropertyTraces = [];
            dbContext.Properties.Add(property2);

            var property3 = PropertyMockingData.GetProperty();
            property3.Price = 300000M;  // Price outside the filter range
            property3.OwnerId = ownerToAdd.OwnerId;
            property3.Owner = ownerToAdd;
            property3.PropertyImages = [];
            property3.PropertyTraces = [];
            dbContext.Properties.Add(property3);

            await dbContext.SaveChangesAsync();

            // Define the filters
            var filters = new PropertyFilterRequest
            {
                MinPrice = 50000M,
                MaxPrice = 200000M
            };

            // Act
            var response = await _client.GetAsync($"{PropertyRoute}?minPrice={filters.MinPrice}&maxPrice={filters.MaxPrice}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<IEnumerable<PropertyView>>>();
            actionDataResponse.Should().NotBeNull();
            actionDataResponse?.Data.Should().HaveCountGreaterThan(0);
            actionDataResponse?.Data.Any(p => p.Price > filters.MaxPrice).Should().BeFalse();

            // Clean up
            dbContext.Properties.Remove(property1);
            dbContext.Properties.Remove(property2);
            dbContext.Properties.Remove(property3);
            dbContext.Owners.Remove(ownerToAdd);
            await dbContext.SaveChangesAsync();
        }
    }
}
