using Moq;
using RainfallApi.Application.Services;
using RainfallApi.Core.Entities;
using Xunit;

namespace RainfallApi.Tests.RainfallApi.Application.Tests
{
    public class RainfallServiceTests
    {
        [Fact]
        public async Task GetRainfallReadingsAsync_ShouldReturnReadings()
        {
            // Arrange
            var service = new RainfallService();

            // Act
            List<RainfallReading> result = await service.GetRainfallReadingsAsync("stationId");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
