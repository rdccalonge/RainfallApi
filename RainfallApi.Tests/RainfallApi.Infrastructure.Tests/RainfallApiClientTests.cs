using Moq;
using Moq.Protected;
using RainfallApi.Infrastructure.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                    HttpResponseMessage response = new HttpResponseMessage();

                    return response;
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://environment.data.gov.uk/")
            };
            RainfallApiClient client = new RainfallApiClient(httpClient);

            // Act
            var result = await client.GetRainfallReadingsAsync("stationId", 10);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
