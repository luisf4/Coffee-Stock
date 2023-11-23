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
        Console.WriteLine("Data from " + symbol + " : " + check);

        // Checks if the info is old if it is make a request and store the data 
        if (check == "ok")
        {
            return stockSql.ReadStock(symbol);
        }
        else if (check == "old")
        {
            using HttpClient client = new();

            // Set the url 
            string url = $"https://brapi.dev/api/quote/{symbol}?modules=summaryProfile,balanceSheetHistory,financialData&range={range}&interval=1d&fundamental=true&dividends=true&token={API_TOKEN_Brapi}";
            // Makes a request 
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Deserialize JSON string to C# object
            StockIn stockInfo = JsonConvert.DeserializeObject<StockIn>(content)!;

            // Access information
            foreach (var result in stockInfo.Results)
            {
                StockData stock = new StockData(
                    result.Symbol,
                    result.ShortName,
                    result.RegularMarketPrice,
                    result.Logourl,
                    formattedDateTime,
                    result.MarketCap,
                    result.RegularMarketVolume,
                    result.SummaryProfile.Industry,
                    result.SummaryProfile.Sector,
                    result.SummaryProfile.LongBusinessSummary,
                    result.SummaryProfile.FullTimeEmployees,
                    result.SummaryProfile.Address1,
                    result.SummaryProfile.City,
                    result.SummaryProfile.Country,
                    result.HistoricalDataPrice,
                    result.dividendsData.cashDividends
                    );
                stockSql.UpdateStock(stock);
                return stock;
            }
            return null!;
        }
        else
        {
            using HttpClient client = new();

            // Set the url 
            string url = $"https://brapi.dev/api/quote/{symbol}?modules=summaryProfile,balanceSheetHistory,financialData&range={range}&interval=1d&fundamental=true&dividends=true&token={API_TOKEN_Brapi}";

            // Makes a request 
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Deserialize JSON string to C# object
            StockIn stockInfo = JsonConvert.DeserializeObject<StockIn>(content)!;

            // Access information
            foreach (var result in stockInfo.Results)
            {
                StockData stock = new StockData(
                    result.Symbol,
                    result.ShortName,
                    result.RegularMarketPrice,
                    result.Logourl,
                    formattedDateTime,
                    result.MarketCap,
                    result.RegularMarketVolume,
                    result.SummaryProfile.Industry,
                    result.SummaryProfile.Sector,
                    result.SummaryProfile.LongBusinessSummary,
                    result.SummaryProfile.FullTimeEmployees,
                    result.SummaryProfile.Address1,
                    result.SummaryProfile.City,
                    result.SummaryProfile.Country,
                    result.HistoricalDataPrice,
                    result.dividendsData.cashDividends
                    );
                stockSql.WriteStock(stock);
                return stock;
            }
            return null!;
        }
    }
}
