namespace FormulaAirLine.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string? PassengerEmail { get; set; }
        public string? PassengerPhone { get; set; }
        public string? PassengerAddress { get; set; }
        public string? FlightNumber { get; set; }
        public string? SeatNumber { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}