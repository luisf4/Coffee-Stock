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
            var responseData = AuthService.VerifyJWT(registrationData!);

            _portfolioServices.Create(portfolio, responseData);

            return Ok("ok");
        }
    }

    [HttpGet("{portfolio}")]
    public async Task<IActionResult> ReadPortfolio(string portfolio)
    {
        return Ok();
    }

    [HttpPut("{portfolio}")]
    public async Task<IActionResult> UpdatePortfolio(string portfolio)
    {
        return Ok();
    }

    [HttpDelete("{portfolio}")]
    public async Task<IActionResult> DeletePortfolio(string portfolio)
    {
        return Ok();
    }
}