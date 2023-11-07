using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS config
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("*", "*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// App stuff
var app = builder.Build();
// Use CORS
app.UseCors(MyAllowSpecificOrigins);


// Get stock info
app.MapGet("/stock/{symbol}", (string symbol) =>
{
    StockServices stock = new StockServices();
    return stock.GetStockInfo(symbol,"5d");
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


// Verify the token 
app.MapPost("/auth/jwt", async (HttpContext context) =>
{
    try
    {
        using (var reader = new StreamReader(context.Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var responseData = JsonSerializer.Serialize(AuthService.VerifyJWT(registrationData!));
            Console.WriteLine(responseData);
            Console.WriteLine(registrationData);

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
}
);


// Run
app.Run();
