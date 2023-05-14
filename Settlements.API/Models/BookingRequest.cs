namespace Settlements.API.Models;

public class BookingRequest
{
    private BookingRequest(string name, TimeOnly bookingTime)
    {
        Name = name;
        BookingTime = bookingTime;
    }

    internal static bool TryParse(BookingRequestDto? dto, out BookingRequest? bookingRequest)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Name) || !dto.BookingTime.IsBetween(new TimeOnly(9, 0), new TimeOnly(16, 0)))
        {
            bookingRequest = null;
            return false;
        }

        bookingRequest = new BookingRequest(dto.Name, dto.BookingTime);
        return true;
    }

    public string Name { get; }

    public TimeOnly BookingTime { get; }
}
