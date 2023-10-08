using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Get stock info
app.MapGet("/stock/{symbol}", (string symbol) =>
{
    StockServices stock = new StockServices();
    return stock.GetStockInfo(symbol);
});


// Gets stock sharts
app.MapGet("/stock/{symbol}/sharts/{date}", (string symbol, string date) =>
{
    StockServices stock = new StockServices();
    return stock.GetStockShart(symbol, date);
});


// Register
app.MapPost("/auth/register", async (HttpContext context) =>
{
    try
    {
        using (var reader = new StreamReader(context.Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();

            // Deserialize the JSON data into your model using System.Text.Json
            var registrationData = JsonSerializer.Deserialize<UserDto>(requestBody);

            // Serialize data back to JSON if needed
            var responseData = JsonSerializer.Serialize(AuthService.Register(registrationData!));

            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseData);
        }
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Internal server error: " + ex.Message);
    }
});


// Login
app.MapPost("/auth/login", async (HttpContext context) =>
{
    try
    {
        using (var reader = new StreamReader(context.Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();

            // Deserialize the JSON data into your model using System.Text.Json
            var registrationData = JsonSerializer.Deserialize<UserDto>(requestBody);

            // Serialize data back to JSON if needed
            var responseData = JsonSerializer.Serialize(AuthService.Login(registrationData!));

            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseData);
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions if needed
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Internal server error: " + ex.Message);
    }
});

app.Run();
