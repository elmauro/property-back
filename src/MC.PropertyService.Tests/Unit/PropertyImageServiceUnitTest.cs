using AutoMapper;
using FluentAssertions;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Controllers.v1;
using MC.PropertyService.API.Data.Models;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Options;
using MC.PropertyService.API.Services.v1.Commands;
using MC.PropertyService.API.Services.v1.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.PropertyService.Tests.Unit
{
    [Collection("Integration")]
    public class PropertyImageServiceUnitTest
    {
        private readonly Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<AddPropertyImageHandler>> _loggerAddImageMock;
        private readonly AddPropertyHandler _addPropertyHandler;
        private readonly AddPropertyImageHandler _addPropertyImageHandler;

        public PropertyImageServiceUnitTest()
        {
            _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerAddImageMock = new Mock<ILogger<AddPropertyImageHandler>>();

            _addPropertyImageHandler = new AddPropertyImageHandler(
                _propertyImageRepositoryMock.Object,
                _mapperMock.Object,
                _loggerAddImageMock.Object);
        }

        [Fact]
        public async Task AddPropertyImageAsync_ValidImage_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var propertyImageRequest = new PropertyImageRequest { File = "image.png" };
            var propertyImage = new PropertyImage { PropertyImageId = Guid.NewGuid().ToString(), File = "image.png" };

            _mapperMock.Setup(mapper => mapper.Map<PropertyImage>(propertyImageRequest))
                .Returns(propertyImage);

            _propertyImageRepositoryMock.Setup(repo => repo.AddPropertyImageAsync(It.IsAny<PropertyImage>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addPropertyImageHandler.Handle(new AddPropertyImageCommand(Guid.NewGuid(), propertyImageRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(PropertyImageController.AddImage));
            createdAtActionResult?.RouteValues?["propertyImageId"].Should().Be(propertyImage.PropertyImageId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<PropertyImageRequest>>().Subject;
            response?.Data.Should().Be(propertyImageRequest);
        }

        [Fact]
        public async Task AddPropertyImageAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var propertyImageRequest = new PropertyImageRequest { File = "image.png" };
            _mapperMock.Setup(mapper => mapper.Map<PropertyImage>(propertyImageRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addPropertyImageHandler.Handle(new AddPropertyImageCommand(Guid.NewGuid(), propertyImageRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
