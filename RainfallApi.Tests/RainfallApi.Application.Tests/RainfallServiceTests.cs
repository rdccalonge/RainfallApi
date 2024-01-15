using Moq;
using RainfallApi.Application.Services;
using RainfallApi.Core.Entities;
using RainfallApi.Infrastructure.Clients;
using RainfallApi.Infrastructure.Responses;
using Xunit;

namespace RainfallApi.Tests.RainfallApi.Application.Tests
{
    public class RainfallServiceTests
    {
        [Fact]
        public async Task GetRainfallReadingsAsync_ValidStationId_ReturnsCorrectResponse()
        {
            // Arrange
            var mockApiClient = new Mock<IRainfallApiClient>();
            mockApiClient.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ClientDTO<ResponseDTO<RainfallReadingDTO>>
                {
                    IsSuccess = true,
                    SuccessResponse = new ResponseDTO<RainfallReadingDTO>
                    {
                        Items = new List<RainfallReadingDTO>
                        {
                            new RainfallReadingDTO { DateTime = DateTime.Today, Measure = "test", Value = 15.5m }
                        }
                    }
                });

            var rainfallService = new RainfallService(mockApiClient.Object);

            // Act
            var result = await rainfallService.GetRainfallReadingsAsync(1, 5);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Readings);
            Assert.Single(result.Readings);
            Assert.Equal(15.5m, result.Readings[0].AmountMeasured);
        }

        [Fact]
        public async Task GetRainfallReadingsAsync_NotSuccess_ReturnsNull()
        {
            // Arrange
            var mockApiClient = new Mock<IRainfallApiClient>();
            mockApiClient.Setup(x => x.GetRainfallReadingsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ClientDTO<ResponseDTO<RainfallReadingDTO>>
                {
                    IsSuccess = false,
                });

            var rainfallService = new RainfallService(mockApiClient.Object);

            // Act
            var result = await rainfallService.GetRainfallReadingsAsync(1, 5);

            // Assert
            Assert.Null(result);
        }
    }
}
