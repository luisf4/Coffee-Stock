using System.Text.Json;
using Newtonsoft.Json;


public class StockServices
{
    // Load the api keys from file 
    private string API_TOKEN_Finnhub = CredentialsLoader.LoadApiKeyFinnhub()!;
    private string API_TOKEN_Brapi = CredentialsLoader.LoadApiKeyBrapi()!;
    private string API_TOKEN_Polygon = CredentialsLoader.LoadApiKeyPolygon()!;



    public async Task<StockData> GetStockInfo(string symbol, string range)
    {
        var stockSql = new StockSql();
        var check = stockSql.CheckTimeData(symbol);
        Console.WriteLine(check);
        // Checks if the info is old if it is make a request and store the data 
        if (check == "ok")
        {
            return stockSql.ReadStock(symbol);
        }
        else if (check == "old")
        {
            using HttpClient client = new();

            // Set the url 
            string url = $"https://brapi.dev/api/quote/{symbol}?range={range}&interval=1d&fundamental=true&dividends=false&token={API_TOKEN_Brapi}";

            // Makes a request 
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Deserialize JSON string to C# object
            StockIn stockInfo = JsonConvert.DeserializeObject<StockIn>(content)!;

            // Access information
            foreach (var result in stockInfo.Results)
            {
                StockData stock = new StockData(result.Symbol, result.ShortName, result.RegularMarketPrice, result.Logourl, formattedDateTime, result.historicalDataPrice);
                stockSql.UpdateStock(stock);
            }
            return null!;
        }
        else
        {
            using HttpClient client = new();

            // Set the url 
            string url = $"https://brapi.dev/api/quote/{symbol}?range={range}&interval=1d&fundamental=true&dividends=false&token={API_TOKEN_Brapi}";

            // Makes a request 
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Deserialize JSON string to C# object
            StockIn stockInfo = JsonConvert.DeserializeObject<StockIn>(content)!;

            // Access information
            foreach (var result in stockInfo.Results)
            {
                StockData stock = new StockData(result.Symbol, result.ShortName, result.RegularMarketPrice, result.Logourl, formattedDateTime, result.historicalDataPrice);
                stockSql.WriteStock(stock);
            }
            return null!;
        }



    }


}
