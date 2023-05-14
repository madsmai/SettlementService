using Microsoft.AspNetCore.Mvc.Testing;
using Settlements.API.Models;

namespace Settlements.Test.Integration;
public class BookingTests
    : IClassFixture<WebApplicationFactory<API.Program>>
{
    private readonly WebApplicationFactory<API.Program> factory;

    public BookingTests(WebApplicationFactory<API.Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task BookingOutOfHours_Should_Return_BadRequest()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.PostBookingRequest(new BookingRequestDto("John Smith", "19:00"));

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("", "09:00")]
    [InlineData(" ", "09:00")]
    [InlineData("\n", "09:00")]
    public async Task InvalidData_Should_Return_BadRequest(string name, string time)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.PostBookingRequest(new BookingRequestDto(name, time));

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RequestingBooking_FiveTimes_Should_Return_Conflict()
    {
        // Arrange
        var client = factory.CreateClient();
        var requests = new List<Task<HttpResponseMessage>>();

        // Act
        for (int j = 0; j < 5; j++)
        {
            requests.Add(client.PostBookingRequest(new BookingRequestDto("John Smith", "09:00")));
        }

        var results = await Task.WhenAll(requests);

        // Assert
        results.Count(r => r.StatusCode == System.Net.HttpStatusCode.OK).Should().Be(4);
        results.Count(r => r.StatusCode == System.Net.HttpStatusCode.Conflict).Should().Be(1);
    }
}
