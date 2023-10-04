using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class StockServices
{
    // Load the api keys from file 
    private string API_TOKEN_Finnhub = CredentialsLoader.LoadApiKeyFinnhub();
    private string API_TOKEN_Polygon = CredentialsLoader.LoadApiKeyPolygon();


    // Polygon API USE
    //
    //
    //
    public async Task<string> GetStockInfo(string symbol)
    {
        using HttpClient client = new();

        // Set the url 
        string url = $"https://api.polygon.io/v3/reference/tickers/{symbol}?apiKey={API_TOKEN_Polygon}";
        Console.WriteLine();

        // Makes a request 
        var res = await client.GetAsync(url);
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

        // Parse the given date ** dont need it for now 
        // string formattedDate = DateTime.TryParseExact(date, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd") : "Invalid date format";

        // Set the url 
        string url = $"https://api.polygon.io/v2/aggs/ticker/{symbol}/range/1/day/{date}/{dateToday}?adjusted=true&sort=asc&limit=10&apiKey={API_TOKEN_Polygon}";
        
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

    // Finnhub API USE 
    //
    //
    //
}
