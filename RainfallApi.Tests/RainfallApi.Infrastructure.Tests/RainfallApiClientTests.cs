using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RainfallApi.Infrastructure.Clients;
using RainfallApi.Infrastructure.Responses;
using System.Net;
using Xunit;

namespace RainfallApi.Tests.RainfallApi.Infrastructure.Tests
{
    public class RainfallApiClientTests
    {
        [Fact]
        public async Task GetRainfallReadingsAsync_ReturnsSuccessResponse_WhenHttpClientReturnsSuccess()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(JsonConvert.SerializeObject(new RainfallReadingDTO { }))
                    };
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://environment.data.gov.uk/")
            };
            RainfallApiClient client = new RainfallApiClient(httpClient);

            // Act
            var result = await client.GetRainfallReadingsAsync(500, 10);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.BadGateway)]
        public async Task GetRainfallReadingsAsync_ReturnsFailResponse_WhenHttpClientReturnsFailStatusCodes(HttpStatusCode failStatusCode)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = failStatusCode,
                        Content = new StringContent(JsonConvert.SerializeObject(new RainfallReadingDTO { }))
                    };
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://environment.data.gov.uk/")
            };
            RainfallApiClient client = new RainfallApiClient(httpClient);

            // Act
            var result = await client.GetRainfallReadingsAsync(500, 10);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
