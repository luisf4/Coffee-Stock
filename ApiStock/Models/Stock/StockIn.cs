public class HistoricalDataPrice
{
    public long Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public long Volume { get; set; }
    public double AdjustedClose { get; set; }
}

public class Stock
{
    public required string Symbol { get; set; }
    public required string Currency { get; set; }
    public double TwoHundredDayAverage { get; set; }
    public double TwoHundredDayAverageChange { get; set; }
    public double TwoHundredDayAverageChangePercent { get; set; }
    public long MarketCap { get; set; }
    public required string ShortName { get; set; }
    public required string LongName { get; set; }
    public double RegularMarketChange { get; set; }
    public double RegularMarketChangePercent { get; set; }
    public required string RegularMarketTime { get; set; }
    public double RegularMarketPrice { get; set; }
    public double RegularMarketDayHigh { get; set; }
    public required string RegularMarketDayRange { get; set; }
    public double RegularMarketDayLow { get; set; }
    public long RegularMarketVolume { get; set; }
    public double RegularMarketPreviousClose { get; set; }
    public double RegularMarketOpen { get; set; }
    public double AverageDailyVolume3Month { get; set; }
    public double AverageDailyVolume10Day { get; set; }
    public double FiftyTwoWeekLowChange { get; set; }
    public double FiftyTwoWeekLowChangePercent { get; set; }
    public required string FiftyTwoWeekRange { get; set; }
    public double FiftyTwoWeekHighChange { get; set; }
    public double FiftyTwoWeekHighChangePercent { get; set; }
    public double FiftyTwoWeekLow { get; set; }
    public double FiftyTwoWeekHigh { get; set; }
    public double PriceEarnings { get; set; }
    public double EarningsPerShare { get; set; }
    public required string LogoUrl { get; set; }
    public required string UpdatedAt { get; set; }
    public required string UsedInterval { get; set; }
    public required string UsedRange { get; set; }
    public required List<HistoricalDataPrice> HistoricalDataPrice { get; set; }
    public required List<string> ValidRanges { get; set; }
    public required List<string> ValidIntervals { get; set; }
}

public class StockIn
{
    public required List<Stock> Results { get; set; }
    public DateTime RequestedAt { get; set; }
    public required string Took { get; set; }
}
