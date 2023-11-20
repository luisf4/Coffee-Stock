// AuthController.cs

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.Json;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        try
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var registrationData = JsonSerializer.Deserialize<UserDto>(requestBody);
                var responseData = JsonSerializer.Serialize(AuthService.Register(registrationData!));

                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "application/json";
                Console.WriteLine(Response);
                return Content(responseData);
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Content("Internal server error: " + ex.Message);
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        try
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var registrationData = JsonSerializer.Deserialize<UserDto>(requestBody);
                var responseData = JsonSerializer.Serialize(AuthService.Login(registrationData!));

                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "application/json";
                return Content(responseData);
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Content("Internal server error: " + ex.Message);
        }
    }

    [HttpPost("jwt")]
    public async Task<IActionResult> VerifyJWT()
    {
        try
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
                var responseData = JsonSerializer.Serialize(AuthService.VerifyJWT(registrationData!));

                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "application/json";
                return Content(responseData);
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Content("Internal server error: " + ex.Message);
        }
    }

    [HttpPost("User")]
    public async Task<IActionResult> GetUsername()
    {
        try
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
                var responseData = JsonSerializer.Serialize(AuthService.GetUsername(registrationData!.jwtToken));

                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "application/json";
                return Content(responseData);
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Content("Internal server error: " + ex.Message);
        }
    }
}
