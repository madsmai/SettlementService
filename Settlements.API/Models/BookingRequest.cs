using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Settlements.API.Models;

public class BookingRequest
{
    private BookingRequest(string name, TimeOnly bookingTime, TimeOnly bookingEndTime)
    {
        Name = name;
        BookingStartTime = bookingTime;
        BookingEndTime = bookingEndTime;
    }

    internal static bool TryParse(BookingRequestDto? dto, [NotNullWhen(true)] out BookingRequest? bookingRequest)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Name))
        {
            bookingRequest = null;
            return false;
        }

        if (!TimeOnly.TryParse(dto.BookingTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bookingStartTime) ||
            !bookingStartTime.IsBetween(new TimeOnly(9, 0), new TimeOnly(16, 0)))
        {
            bookingRequest = null;
            return false;
        }

        bookingRequest = new BookingRequest(dto.Name, bookingStartTime, bookingStartTime.AddMinutes(59));
        return true;
    }

    public string Name { get; }

    public TimeOnly BookingStartTime { get; }

    public TimeOnly BookingEndTime { get; }
}
