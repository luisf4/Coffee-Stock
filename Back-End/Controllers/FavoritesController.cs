using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesServices _favoriteServices;

    public FavoritesController()
    {
        _favoriteServices = new FavoritesServices();
    }

    [HttpPost("{symbol}/{user}")]
    public IActionResult AddFavorites(string symbol, string user)
    {
        _favoriteServices.AddFavorites(symbol, user);
        return Ok("result");
    }

    [HttpDelete("{symbol}/{user}")]
    public IActionResult DeleteFavorites(string symbol, string user)
    {
        _favoriteServices.DeleteFavorites(symbol, user);
        return Ok();
    }

    [HttpGet("{user}")]
    public IActionResult GetFavorites(string user)
    {
        var res = _favoriteServices.GetFavorites(user);
        var Json = JsonSerializer.Serialize(res);
        return Content(Json);
    }
}
