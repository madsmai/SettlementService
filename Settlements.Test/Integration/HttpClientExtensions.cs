using System.Net.Http.Json;
using Settlements.API.Models;

namespace Settlements.Test.Integration;
internal static class HttpClientExtensions
{
    internal static async Task<HttpResponseMessage> PostBookingRequest(this HttpClient client, BookingRequestDto dto)
    {
        using var content = JsonContent.Create(dto, typeof(BookingRequestDto));
        return await client.PostAsync(new Uri("/Booking", UriKind.Relative), content);
    }
}
