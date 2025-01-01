using FormulaAirLine.API.Models;
using FormulaAirLine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirLine.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingEndpoints : ControllerBase
{
    private readonly ILogger<BookingEndpoints> _logger;
    private readonly IMessageProducer _messageProducer;

    //In-Memory db
    private static readonly List<Booking> _bookings = new();

    public BookingEndpoints(ILogger<BookingEndpoints> logger, IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking([FromBody] Booking booking)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        _bookings.Add(booking);

        _messageProducer.SendingMessage(booking);

        return Ok();
    }
}