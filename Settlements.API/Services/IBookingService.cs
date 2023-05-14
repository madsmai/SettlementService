using System.Diagnostics.CodeAnalysis;
using Settlements.API.Models;

namespace Settlements.API.Services;
public interface IBookingService
{
    bool TryAddBooking(BookingRequest request, [NotNullWhen(true)] out BookingResponse? bookingResponse);
}
