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
            return "Error"; // You can also return an error message or throw an exception
        }
    }

    public async Task<String> GetStockShart(string symbol, string date)
    {   
        // Creates HttpClient
        using HttpClient client = new();

        // Gets date 
        var dateToday = DateTime.Now.ToString("yyyy-MM-dd");

        // Parse the given date 
        // string formattedDate = DateTime.TryParseExact(date, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd") : "Invalid date format";

        // Set the url 
        string url = $"https://api.polygon.io/v2/aggs/ticker/{symbol}/range/1/day/{date}/{dateToday}?adjusted=true&sort=asc&limit=120&apiKey={API_TOKEN}";
        
        // Makes a request 
        var res = await client.GetAsync(url);
        Console.WriteLine(url);
        if (res.IsSuccessStatusCode)
        {
            // Read and returns a Json
            var content = await res.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            Console.WriteLine("Cant handle GetStockSharts");
            return $"Error: {res.StatusCode}";
        }
    }
}
