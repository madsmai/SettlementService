using System.Diagnostics.CodeAnalysis;
using Settlements.API.Models;

namespace Settlements.API.Services;

public class BookingService : IBookingService
{
    private readonly object bookingsLock = new object();
    private readonly IList<BookingRequest> bookings;

    public BookingService()
    {
        this.bookings = new List<BookingRequest>();
    }

    public BookingService(IList<BookingRequest> bookings)
    {
        this.bookings = bookings;
    }

    public bool TryAddBooking(BookingRequest request, [NotNullWhen(true)] out BookingResponse? bookingResponse)
    {
        lock (bookingsLock)
        {
            var overlappingBookings = bookings.Where(booking => booking.BookingStartTime.IsBetween(request.BookingStartTime, request.BookingEndTime) ||
                            booking.BookingEndTime.IsBetween(request.BookingStartTime, request.BookingEndTime));
            if (overlappingBookings.Count() >= 4)
            {
                bookingResponse = null;
                return false;
            }

            bookings.Add(request);
            bookingResponse = new BookingResponse();
        }

        return true;
    }
}
