using FormulaAirLine.API.Models;
using FormulaAirLine.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FormulaAirLine.API.Endpoints;

public static class BookingEndpoints
{
    // In-Memory db
    private static readonly List<Booking> _bookings = new();

    public static void MapBookingEndpoints(this WebApplication app)
    {
        app.MapPost("/bookings", (Booking booking, IMessageProducer messageProducer, ILogger<Program> logger) =>
        {
            if (booking is null)
                return Results.BadRequest("Invalid booking data.");

            _bookings.Add(booking);
            messageProducer.SendingMessage(booking);
            logger.LogInformation("Booking created successfully.");

            return Results.Ok();
        })
        .WithName("CreateBooking")
        .WithOpenApi();
    }
}