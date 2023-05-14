namespace Settlements.API.Models;

public record BookingRequestDto
{
    public BookingRequestDto(string name, string bookingTime)
    {
        Name = name;
        BookingTime = bookingTime;
    }

    public string Name { get; }

    public string BookingTime { get; }
}
