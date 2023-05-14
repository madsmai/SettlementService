using Microsoft.AspNetCore.Mvc.Testing;
using Settlements.API.Models;

namespace Settlements.Test.Integration;
public class BookingTests
    : IClassFixture<WebApplicationFactory<Settlements.API.Program>>
{
    private readonly WebApplicationFactory<Settlements.API.Program> factory;

    public BookingTests(WebApplicationFactory<Settlements.API.Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task BookingOutOfHours_Should_Return_BadRequest()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.PostBookingRequest(new BookingRequestDto("John Smith", new TimeOnly(19, 0)));

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("", 9, 0)]
    [InlineData(" ", 9, 0)]
    [InlineData("\n", 9, 0)]
    public async Task InvalidData_Should_Return_BadRequest(string name, int hours, int minutes)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.PostBookingRequest(new BookingRequestDto(name, new TimeOnly(hours, minutes)));

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }
}
