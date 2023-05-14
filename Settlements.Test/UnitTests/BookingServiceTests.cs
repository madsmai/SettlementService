using Settlements.API.Models;
using Settlements.API.Services;

namespace Settlements.Test.UnitTests;
public class BookingServiceTests
{
    private static List<BookingRequest> GetFilledSchedule()
    {
        var schedule = new List<BookingRequest>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (BookingRequest.TryParse(new BookingRequestDto($"John {j}", $"{i + 9}:00"), out var bookingRequest))
                {
                    schedule.Add(bookingRequest);
                }
            }
        }

        return schedule;
    }

    [Fact]
    public void RequestingBooking_When_ScheduleIsFull_Should_ReturnFalse()
    {
        // Arrange
        IFixture fixture = FixtureFactory.Create();
        fixture.Customizations.Add(new BookingRequestDtoSpecimenBuilder());
        var bookingRequestDto = fixture.Create<BookingRequestDto>();
        var sut = new BookingService(GetFilledSchedule());
        BookingRequest.TryParse(bookingRequestDto, out var bookingRequest).Should().BeTrue();

        // Act
        var result = sut.TryAddBooking(bookingRequest!, out var bookingResponse);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void RequestingBooking_FiveTimes_Should_ReturnFalse()
    {
        // Arrange
        IFixture fixture = FixtureFactory.Create();
        fixture.Customizations.Add(new BookingRequestDtoSpecimenBuilder());
        var bookingRequestDto = fixture.Create<BookingRequestDto>();
        var sut = new BookingService();
        BookingRequest.TryParse(bookingRequestDto, out var bookingRequest).Should().BeTrue();

        // Act
        bool result = true;
        for (int j = 0; j < 5; j++)
        {
            result = sut.TryAddBooking(bookingRequest!, out var bookingResponse);
        }

        // Assert
        result.Should().BeFalse();
    }
}
