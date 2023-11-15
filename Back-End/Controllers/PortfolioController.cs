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
        var result = _portfolioServices.Create(portfolio);
        return Ok();
    }
}