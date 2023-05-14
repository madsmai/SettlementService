namespace Settlements.API.Models;

public record BookingResponse
{
    public BookingResponse()
    {
        BookingId = Guid.NewGuid();
    }

    public Guid BookingId { get; }
}
