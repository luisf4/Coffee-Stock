using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly PortfolioServices _portfolioServices;

    public PortfolioController()
    {
        _portfolioServices = new PortfolioServices();
    }

    [HttpPost("{portfolio}")]
    public async Task<IActionResult> CreatePortfolio(string portfolio)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var username = AuthService.GetUsername(registrationData!.jwtToken);
            _portfolioServices.Create(portfolio, username);

            return Ok("ok");
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> ReadPortfolios()
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var username = AuthService.GetUsername(registrationData!.jwtToken);
            var portfolios = _portfolioServices.ReadAll(username);
            var portfoliosJson = JsonSerializer.Serialize(portfolios);
            Console.WriteLine(portfoliosJson);
            return Content(portfoliosJson);
        }
    }

    [HttpPut("{portfolio}/{newName}")]
    public async Task<IActionResult> UpdatePortfolio(int portfolio, string newName)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var username = AuthService.GetUsername(registrationData!.jwtToken);
            _portfolioServices.Update(username, portfolio, newName);
            return Ok();
        }
    }

    [HttpDelete("{portfolio}")]
    public async Task<IActionResult> DeletePortfolio(int portfolio)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var username = AuthService.GetUsername(registrationData!.jwtToken);
            _portfolioServices.Delete(username, portfolio);
            return Ok();
        }
    }
}