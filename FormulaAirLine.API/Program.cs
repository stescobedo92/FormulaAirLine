using FormulaAirLine.API.Endpoints;
using FormulaAirLine.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios necesarios
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddSingleton<IMessageProducer, MessageProducer>();

var app = builder.Build();

// Configurar Swagger en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar HTTPS redirection
app.UseHttpsRedirection();

// Mapear los endpoints de Booking
app.MapBookingEndpoints();

app.Run();