using System;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class StockServices
{
    // Load the api keys from file 
    private string API_TOKEN_Finnhub = CredentialsLoader.LoadApiKeyFinnhub()!;
    private string API_TOKEN_Brapi = CredentialsLoader.LoadApiKeyBrapi()!;
    private string API_TOKEN_Polygon = CredentialsLoader.LoadApiKeyPolygon()!;


  
    public async Task<string> GetStockInfo(string symbol,string range)
    {
        using HttpClient client = new();

        // Set the url 
        string url = $"https://brapi.dev/api/quote/{symbol}?range={range}&interval=1d&fundamental=true&dividends=false&token={API_TOKEN_Brapi}";

        // Makes a request 
        var res = await client.GetAsync(url);
        var content = await res.Content.ReadAsStringAsync();
        // Console.WriteLine(content);

        // dynamic data = JsonConvert.DeserializeObject(content)!;
        // dynamic json = JsonConvert.DeserializeObject(data.results[0])!;

        // convert to stuff
        // ChartData chart = JsonConvert.DeserializeObject<ChartData>(json);
        // var stock = JsonConvert.DeserializeObject<Root>(content);
        // Console.WriteLine(stock.Results);

        if (res.IsSuccessStatusCode)
        {
            return content; // Return JSON content as a string
        }
        else
        {
            // Handle error response here
            return "Error"; // You can also return an error message or throw an exception
        }
    }


}
