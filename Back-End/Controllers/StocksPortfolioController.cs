using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class StocksPortfolioController : ControllerBase
{
    private readonly StocksPortfolioServices _stocksPortfolioServices;

    public StocksPortfolioController()
    {
        _stocksPortfolioServices = new StocksPortfolioServices();
    }

    [HttpPost()]
    public async Task<IActionResult> Add()
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var stocksPortfolioInfo = JsonSerializer.Deserialize<StocksPortfolio>(requestBody);
            await _stocksPortfolioServices.Add(stocksPortfolioInfo!);
            return Ok("ok");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read(int id)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
            var username = AuthService.GetUsername(registrationData!.jwtToken);
            var portfolioStocks =  _stocksPortfolioServices.Read(username,id);
            var portfolioStocksJson = JsonSerializer.Serialize(portfolioStocks);
            return Content(portfolioStocksJson);
        }
    }

    // [HttpPut("{portfolio}/{newName}")]
    // public async Task<IActionResult> Update(int portfolio, string newName)
    // {
    //     using (var reader = new StreamReader(Request.Body))
    //     {
    //         var requestBody = await reader.ReadToEndAsync();
    //         var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
    //         var username = AuthService.GetUsername(registrationData!);
    //         _stocksPortfolioServices.Update(username, portfolio, newName);
    //         return Ok();
    //     }
    // }

    // [HttpDelete("{portfolio}")]
    // public async Task<IActionResult> Delete(int portfolio)
    // {
    //     using (var reader = new StreamReader(Request.Body))
    //     {
    //         var requestBody = await reader.ReadToEndAsync();
    //         var registrationData = JsonSerializer.Deserialize<JwtData>(requestBody);
    //         var username = AuthService.GetUsername(registrationData!);
    //         _stocksPortfolioServices.Delete(username, portfolio);
    //         return Ok();
    //     }
    // }
}