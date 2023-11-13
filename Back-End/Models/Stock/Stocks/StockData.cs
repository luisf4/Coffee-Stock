public class StockData
{
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Logo { get; set; }
    public string RequestedAt { get; set; }
    public List<HistoricalData> HistoricalDataPrice { get; set; }

    // Constructor
    public StockData(string symbol, string name, double price, string logo, string requestedAt, List<HistoricalData> historicalDataPrice)
    {
        this.Symbol = symbol;
        this.Name = name;
        this.Price = price;
        this.Logo = logo;
        this.RequestedAt = requestedAt;
        this.HistoricalDataPrice = historicalDataPrice;
    }
}