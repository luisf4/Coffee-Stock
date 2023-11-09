using System;
using System.Collections.Generic;

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

public class Result
{
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public double TwoHundredDayAverage { get; set; }
    public double TwoHundredDayAverageChange { get; set; }
    public double TwoHundredDayAverageChangePercent { get; set; }
    public long MarketCap { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public double RegularMarketChange { get; set; }
    public double RegularMarketChangePercent { get; set; }
    public string RegularMarketTime { get; set; }
    public double RegularMarketPrice { get; set; }
    public double RegularMarketDayHigh { get; set; }
    public double RegularMarketDayLow { get; set; }
    public long RegularMarketVolume { get; set; }
    public double RegularMarketPreviousClose { get; set; }
    public double RegularMarketOpen { get; set; }
    public long AverageDailyVolume3Month { get; set; }
    public long AverageDailyVolume10Day { get; set; }
    public double FiftyTwoWeekLowChange { get; set; }
    public object FiftyTwoWeekLowChangePercent { get; set; }
    public string FiftyTwoWeekRange { get; set; }
    public double FiftyTwoWeekHighChange { get; set; }
    public double FiftyTwoWeekHighChangePercent { get; set; }
    public double FiftyTwoWeekLow { get; set; }
    public double FiftyTwoWeekHigh { get; set; }
    public double PriceEarnings { get; set; }
    public double EarningsPerShare { get; set; }
    public string LogoUrl { get; set; }
    public string UpdatedAt { get; set; }
    public string UsedInterval { get; set; }
    public string UsedRange { get; set; }
    public List<HistoricalDataPrice> HistoricalDataPrice { get; set; }
    public List<string> ValidRanges { get; set; }
    public List<string> ValidIntervals { get; set; }
}

public class Root
{
    public List<Result> Results { get; set; }
    public DateTime RequestedAt { get; set; }
    public string Took { get; set; }
}
