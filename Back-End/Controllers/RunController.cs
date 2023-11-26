using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class RunController : ControllerBase
{
    private readonly StockServices _stockServices;

    public RunController()
    {
        _stockServices = new StockServices();
    }

    [HttpGet]   
    public async Task<IActionResult> GetStockInfo()
    {
        string symbol = "petr4";
        var result = await _stockServices.GetStockInfo(symbol.ToUpper(), "1y");
        return Ok(result);
    }
}
