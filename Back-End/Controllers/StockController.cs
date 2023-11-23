using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly StockServices _stockServices;

    public StockController()
    {
        _stockServices = new StockServices();
    }

    [HttpGet("{symbol}")]   
    public async Task<IActionResult> GetStockInfo(string symbol)
    {
        var result = await _stockServices.GetStockInfo(symbol.ToUpper(), "1mo");
        return Ok(result);
    }
}
