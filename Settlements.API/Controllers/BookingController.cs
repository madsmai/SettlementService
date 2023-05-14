using Microsoft.AspNetCore.Mvc;
using Settlements.API.Models;

namespace Settlements.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> logger;

        public BookingController(ILogger<BookingController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BookingResponse> Post(BookingRequestDto dto)
        {
            if (!BookingRequest.TryParse(dto, out var bookingRequest))
            {
                return new BadRequestResult();
            }

            return new BookingResponse();
        }
    }
}
