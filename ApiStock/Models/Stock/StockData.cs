public class StockData
{
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public double TwoHundredDayAverage { get; set; }
    public double TwoHundredDayAverageChange { get; set; }
    public double TwoHundredDayAverageChangePercent { get; set; }
    public string MarketCap { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public double RegularMarketChange { get; set; }
    public double RegularMarketChangePercent { get; set; }
    public string RegularMarketTime { get; set; }
    public double RegularMarketPrice { get; set; }
    public double RegularMarketDayHigh { get; set; }
    public string RegularMarketDayRange { get; set; }
    public double RegularMarketDayLow { get; set; }
    public long RegularMarketVolume { get; set; }
    public double RegularMarketPreviousClose { get; set; }
    public double RegularMarketOpen { get; set; }
    public int AverageDailyVolume3Month { get; set; }
    public int AverageDailyVolume10Day { get; set; }
    public double FiftyTwoWeekLowChange { get; set; }
    public double FiftyTwoWeekLowChangePercent { get; set; }
    public string FiftyTwoWeekRange { get; set; }
    public double FiftyTwoWeekHighChange { get; set; }
    public double FiftyTwoWeekHighChangePercent { get; set; }
    public double FiftyTwoWeekLow { get; set; }
    public double FiftyTwoWeekHigh { get; set; }
    public double? PriceEarnings { get; set; }
    public double? EarningsPerShare { get; set; }
    public string LogoUrl { get; set; }
    public string UpdatedAt { get; set; }
    public string UsedInterval { get; set; }
    public string UsedRange { get; set; }
    public List<HistoricalData> HistoricalDataPrice { get; set; }
    public List<string> ValidRanges { get; set; }
    public List<string> ValidIntervals { get; set; }
}

public class HistoricalData
{
    public long Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public long Volume { get; set; }
    public double AdjustedClose { get; set; }
}
