using FluentAssertions;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data;
using MC.PropertyService.API.Data.Models;
using MC.PropertyService.API.Options;
using MC.PropertyService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MC.PropertyService.Tests.Integration.v1
{
    public class PropertyImageControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly string _token;
        private const string PropertyImageRoute = "v1/properties";

        public PropertyImageControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _token = AuthenticationMockingData.GenerateJwtToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [Fact]
        public async Task AddPropertyImage_ReturnsBadRequest()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            // Act - Send a request without a file
            var content = new MultipartFormDataContent(); // Empty content
            var response = await _client.PostAsync($"{PropertyImageRoute}/{propertyId}/images", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task AddPropertyImageReturnsOk()
        {
            // Arrange
            var propertyImageToAdd = PropertyImageMockingData.GetPropertyImageRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PropertyDBContext>();

            // Create a property and owner for the property image
            var propertyToAdd = PropertyMockingData.GetProperty();
            propertyToAdd.PropertyImages = new List<PropertyImage>();
            propertyToAdd.PropertyTraces = new List<PropertyTrace>();

            var ownerToAdd = OwnerMockingData.GetOwner();
            ownerToAdd.Properties = new List<Property>();

            // Add owner to the database
            dbContext.Owners.Add(ownerToAdd);
            await dbContext.SaveChangesAsync();

            // Assign the owner to the property
            propertyToAdd.OwnerId = ownerToAdd.OwnerId;
            propertyToAdd.Owner = ownerToAdd;

            // Add property to the database
            dbContext.Properties.Add(propertyToAdd);
            await dbContext.SaveChangesAsync();

            // Prepare the file to upload as IFormFile
            var fileContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Test File Content"));
            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = propertyImageToAdd.File
            };
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            var content = new MultipartFormDataContent
            {
                { fileContent, "file", propertyImageToAdd.File }
            };

            // Act: Send the POST request with the file as multipart/form-data
            var response = await _client.PostAsync($"v1/properties/{propertyToAdd.PropertyId}/images", content);

            // Assert the response status is 201 Created
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            // Clean up: remove the added data from the database
            var propertyImageToRemove = await dbContext.PropertyImages.FirstOrDefaultAsync(pi => pi.File == propertyImageToAdd.File);
            if (propertyImageToRemove != null)
            {
                dbContext.PropertyImages.Remove(propertyImageToRemove);
            }
            dbContext.Properties.Remove(propertyToAdd);
            dbContext.Owners.Remove(ownerToAdd);
            await dbContext.SaveChangesAsync();
        }

    }
}
