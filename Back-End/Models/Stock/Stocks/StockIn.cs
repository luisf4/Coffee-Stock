public class StockIn
{
    public List<Result> Results { get; set; }
    public DateTime RequestedAt { get; set; }
    public string Took { get; set; }
}

public class Result
{
    public string Symbol { get; set; }
    public string ShortName { get; set; }
    public double RegularMarketPrice { get; set; }
    public string Logourl { get; set; }
    public List<HistoricalData> historicalDataPrice { get; set; }
    // Add other properties as needed
}
public class HistoricalData
{
    public int Date { get; set; } // Change from long to DateTime
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public int Volume { get; set; }
    public double AdjustedClose { get; set; }
}