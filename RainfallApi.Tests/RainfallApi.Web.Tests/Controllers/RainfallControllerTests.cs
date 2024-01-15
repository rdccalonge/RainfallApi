using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RainfallApi.Core.Entities;
using RainfallApi.Core.Interfaces;
using RainfallApi.Web.Controllers;
using RainfallApi.Web.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RainfallApi.Tests.RainfallApi.Web.Tests.Controllers
{
    public class RainfallControllerTests
    {
        [Fact]
        public async Task GetRainfallReadings_SuccessfulResponse_ReturnsOkResult()
        {
            // Arrange
            var mockRainfallService = new Mock<IRainfallService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new RainfallController(mockRainfallService.Object, mockMapper.Object);

            var validStationId = "1638";
            var count = 10;

            var successfulResponse = new RainfallReadingResponseModel
            {
                Readings = new List<RainfallReading>
                {
                    new RainfallReading { AmountMeasured = 15.5m, DateMeasured = DateTime.Now },
                }
            };

            mockRainfallService.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(successfulResponse);

            mockMapper.Setup(x => x.Map<RainfallReadingResponseModel, RainfallReadingResponse>(successfulResponse))
                .Returns(new RainfallReadingResponse());

            // Act
            var result = await controller.GetRainfallReadings(validStationId, count);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RainfallReadingResponse>(okResult.Value);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetRainfallReadings_EmptyResponse_ReturnsNoContentResult()
        {
            // Arrange
            var mockRainfallService = new Mock<IRainfallService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new RainfallController(mockRainfallService.Object, mockMapper.Object);

            var validStationId = "1638";
            var count = 10;

            var emptyResponse = new RainfallReadingResponseModel
            {
                Readings = new List<RainfallReading>()
            };

            mockRainfallService.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(emptyResponse);

            // Act
            var result = await controller.GetRainfallReadings(validStationId, count);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, internalServerErrorResult.StatusCode);
            var errorResponse = Assert.IsType<ErrorResponse>(internalServerErrorResult.Value);
            Assert.NotNull(errorResponse);
        }

        [Fact]
        public async Task GetRainfallReadings_InvalidStationId_ReturnsBadRequestResult()
        {
            // Arrange
            var mockRainfallService = new Mock<IRainfallService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new RainfallController(mockRainfallService.Object, mockMapper.Object);

            var invalidStationId = "invalidStationId";
            var count = 10;

            mockRainfallService.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Invalid stationId"));

            // Act
            var result = await controller.GetRainfallReadings(invalidStationId, count);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
            Assert.NotNull(errorResponse);
        }

        [Fact]
        public async Task GetRainfallReadings_NoReadingsFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRainfallService = new Mock<IRainfallService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new RainfallController(mockRainfallService.Object, mockMapper.Object);

            var validStationId = "1623";
            var count = 10;

            // Set up a sample response with no readings found
            mockRainfallService.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((RainfallReadingResponseModel)null);

            // Act
            var result = await controller.GetRainfallReadings(validStationId, count);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
            Assert.NotNull(errorResponse);
        }

        [Fact]
        public async Task GetRainfallReadings_InternalServerError_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var mockRainfallService = new Mock<IRainfallService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new RainfallController(mockRainfallService.Object, mockMapper.Object);

            var validStationId = "12313";
            var count = 10;

            // Set up a sample response for internal server error
            mockRainfallService.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Internal server error"));

            // Act
            var result = await controller.GetRainfallReadings(validStationId, count);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
            var errorResponse = Assert.IsType<ErrorResponse>(internalServerErrorResult.Value);
            Assert.NotNull(errorResponse);
        }
    }
}
