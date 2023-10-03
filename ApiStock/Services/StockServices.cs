using System;
using System.Net.Http;
using System.Threading.Tasks;

public class StockServices
{
    private const string API_TOKEN = "Wr9Uwoumm9VihRtG_JOl7p6vXEpM90fM";

    public async Task<string> GetStockBySymbol(string symbol)
    {
        using HttpClient client = new();

        // Set the request URL
        var res = await client.GetAsync($"https://api.polygon.io/v2/aggs/ticker/{symbol}/range/1/day/2023-01-09/2023-01-09?apiKey={API_TOKEN}");
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadAsStringAsync();
            return content; // Return JSON content as a string
        }
        else
        {
            // Handle error response here
            return null; // You can also return an error message or throw an exception
        }
    }

    public async Task<String> GetStockShart(string symbol, string date){ 
        using HttpClient client = new();

        var dateToday = DateTime.Now.ToString("MM/dd/yyyy");
        var res = await client.GetAsync($"https://api.polygon.io/v2/aggs/ticker/{symbol}/range/1/day/{date}/{dateToday}?adjusted=true&sort=asc&limit=120&apiKey={API_TOKEN}");
    
        if (res.IsSuccessStatusCode) {
            var content = await res.Content.ReadAsStringAsync();
            return content;
        }
        else 
        {   
            Console.WriteLine("Cant handle GetStockSharts");
            return null;
        }
    }
}
