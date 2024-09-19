using AutoMapper;
using FluentAssertions;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Controllers.v1;
using MC.PropertyService.API.Data.Models;
using MC.PropertyService.API.Data.Repositories;
using MC.PropertyService.API.Options;
using MC.PropertyService.API.Services.v1.Commands;
using MC.PropertyService.API.Services.v1.Handlers;
using MC.PropertyService.API.Services.v1.Queries;
using MC.PropertyService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.PropertyService.Tests.Unit
{
    [Collection("Integration")]
    public class PropertyServiceUnitTest
    {
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<AddPropertyHandler>> _loggerAddMock;
        private readonly Mock<ILogger<ChangePropertyPriceHandler>> _loggerChangeMock;
        private readonly Mock<ILogger<UpdatePropertyHandler>> _loggerUpdateMock;
        private readonly Mock<ILogger<ListPropertiesHandler>> _loggerListMock;
        private readonly AddPropertyHandler _addPropertyHandler;
        private readonly ChangePropertyPriceHandler _changePriceHandler;
        private readonly UpdatePropertyHandler _updatePropertyHandler;
        private readonly ListPropertiesHandler _listPropertiesHandler;

        public PropertyServiceUnitTest()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerAddMock = new Mock<ILogger<AddPropertyHandler>>();
            _loggerChangeMock = new Mock<ILogger<ChangePropertyPriceHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdatePropertyHandler>>();
            _loggerListMock = new Mock<ILogger<ListPropertiesHandler>>();

            _addPropertyHandler = new AddPropertyHandler(
                _propertyRepositoryMock.Object,
                _ownerRepositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);

            _changePriceHandler = new ChangePropertyPriceHandler(
                _propertyRepositoryMock.Object,
                _mapperMock.Object,
                _loggerChangeMock.Object);

            _updatePropertyHandler = new UpdatePropertyHandler(
                _propertyRepositoryMock.Object,
                _ownerRepositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);

            _listPropertiesHandler = new ListPropertiesHandler(
                _propertyRepositoryMock.Object,
                _mapperMock.Object,
                _loggerListMock.Object);
        }

        [Fact]
        public async Task CreatePropertyAsync_ValidProperty_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var propertyRequest = new PropertyRequest { Name = "Test Property" };
            var property = new Property { PropertyId = Guid.NewGuid().ToString(), Name = "Test Property" };

            var owner = OwnerMockingData.GetOwner();
            owner.OwnerId = propertyRequest.OwnerId.ToString();

            _mapperMock.Setup(mapper => mapper.Map<Property>(propertyRequest))
                .Returns(property);

            _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(owner);

            _propertyRepositoryMock.Setup(repo => repo.AddPropertyAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addPropertyHandler.Handle(new AddPropertyCommand(propertyRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(PropertyController.Create));
            createdAtActionResult?.RouteValues?["propertyId"].Should().Be(property.PropertyId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<PropertyRequest>>().Subject;
            response?.Data.Should().Be(propertyRequest);
        }

        [Fact]
        public async Task CreatePropertyAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var propertyRequest = new PropertyRequest { Name = "Test Property" };

            var owner = OwnerMockingData.GetOwner();
            owner.OwnerId = propertyRequest.OwnerId.ToString();

            _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(owner);

            _mapperMock.Setup(mapper => mapper.Map<Property>(propertyRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addPropertyHandler.Handle(new AddPropertyCommand(propertyRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task ChangePropertyPriceAsync_ValidProperty_ReturnsNoContent()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var newPrice = 200000m;
            var existingProperty = new Property { PropertyId = propertyId.ToString(), Price = 150000m };

            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync(propertyId.ToString()))
                .ReturnsAsync(existingProperty);

            _propertyRepositoryMock.Setup(repo => repo.UpdatePropertyAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _changePriceHandler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            existingProperty.Price.Should().Be(newPrice);
        }

        [Fact]
        public async Task ChangePropertyPriceAsync_PropertyNotFound_ReturnsNotFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var newPrice = 200000m;

            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync(propertyId.ToString()))
                .ReturnsAsync((Property?)null);

            // Act
            var result = await _changePriceHandler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ChangePropertyPriceAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var newPrice = 200000m;

            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync(propertyId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _changePriceHandler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdatePropertyAsync_ExistingProperty_ReturnsNoContent()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var propertyRequest = new PropertyRequest { Name = "Updated Property" };
            var existingProperty = new Property { PropertyId = propertyId.ToString(), Name = "Existing Property" };
            var updatedProperty = new Property { Name = "Updated Property" };

            var owner = OwnerMockingData.GetOwner();
            owner.OwnerId = propertyRequest.OwnerId.ToString();

            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync(propertyId.ToString()))
                .ReturnsAsync(existingProperty);

            _mapperMock.Setup(mapper => mapper.Map(propertyRequest, existingProperty))
                .Returns(updatedProperty);

            _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(owner);

            _propertyRepositoryMock.Setup(repo => repo.UpdatePropertyAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updatePropertyHandler.Handle(new UpdatePropertyCommand(propertyId, propertyRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdatePropertyAsync_PropertyNotFound_ReturnsNotFound()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var propertyRequest = new PropertyRequest { Name = "Updated Property" };

            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync(propertyId.ToString()))
                .ReturnsAsync((Property?)null);

            // Act
            var result = await _updatePropertyHandler.Handle(new UpdatePropertyCommand(propertyId, propertyRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ListPropertiesAsync_ValidFilters_ReturnsProperties()
        {
            // Arrange
            var filters = new PropertyFilterRequest { MinPrice = 500M, MaxPrice = 1000M };
            var properties = new List<Property>
            {
                new Property { PropertyId = Guid.NewGuid().ToString(), Name = "Property 1", Price = 600M },
                new Property { PropertyId = Guid.NewGuid().ToString(), Name = "Property 2", Price = 800M }
            };
            var totalCount = 2; // Total number of properties that match the filters

            _propertyRepositoryMock.Setup(repo => repo.ListPropertiesAsync(filters, 1, 10))  // Assuming pageNumber=1, pageSize=10 for the test
                .ReturnsAsync((properties, totalCount));

            // Act
            var result = await _listPropertiesHandler.Handle(new ListPropertiesQuery(filters, 1, 10), CancellationToken.None);  // Adding pageNumber and pageSize

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponseList<List<PropertyView>>>().Subject;  // Adjusting to new ActionDataResponseList
            response?.Data.Should().HaveCountGreaterThan(0);
            response?.PageSize.Should().Be(10);
        }


        [Fact]
        public async Task ListPropertiesAsync_NoPropertiesFound_ReturnsEmptyList()
        {
            // Arrange
            var filters = new PropertyFilterRequest { MinPrice = 500M, MaxPrice = 1000M };
            var properties = new List<Property>(); // Empty list of properties
            var totalCount = 0; // Total count should be 0 when no properties match

            _propertyRepositoryMock.Setup(repo => repo.ListPropertiesAsync(filters, 1, 10))  // Assuming pageNumber=1, pageSize=10 for the test
                .ReturnsAsync((properties, totalCount));

            // Act
            var result = await _listPropertiesHandler.Handle(new ListPropertiesQuery(filters, 1, 10), CancellationToken.None);  // Adding pageNumber and pageSize

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
