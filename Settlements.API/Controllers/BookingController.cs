using Microsoft.AspNetCore.Mvc;
using Settlements.API.Models;
using Settlements.API.Services;

namespace Settlements.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public ActionResult<BookingResponse> Post(BookingRequestDto dto)
        {
            if (!BookingRequest.TryParse(dto, out var bookingRequest))
            {
                return new BadRequestResult();
            }

            if (!bookingService.TryAddBooking(bookingRequest, out var bookingResponse))
            {
                return new ConflictResult();
            }

            return bookingResponse;
        }
    }
}
