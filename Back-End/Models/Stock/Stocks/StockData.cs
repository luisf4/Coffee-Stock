using System.Runtime.Intrinsics.X86;

public class StockData
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string? Logo { get; set; }
    public string? RequestedAt { get; set; }

    public decimal? MarketCap { get; set; }
    public decimal? RegularMarketVolume { get; set; }
    public string? Industry { get; set; }
    public string? Sector { get; set; }
    public string? LongBusinessSummary { get; set; }
    public decimal? FullTimeEmployees { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }


    public List<HistoricalDataPrice>? HistoricalDataPrice { get; set; }
    public List<CashDividends>? DividendsData { get; set; }


    
    // Constructor
    public StockData(string? symbol, string? name, double? price, string? logo, string? requestedAt, decimal? MarketCap, decimal? RegularMarketVolume, string? Industry, string? Sector,
     string? LongBusinessSummary, decimal? FullTimeEmployees, string? Address, string? City, string? Country, List<HistoricalDataPrice>? historicalDataPrice, List<CashDividends>? DividendsData)
    {
        this.Symbol = symbol;
        this.Name = name;
        this.Price = price;
        this.Logo = logo;
        this.RequestedAt = requestedAt;
        this.HistoricalDataPrice = historicalDataPrice;
        this.DividendsData = DividendsData;
        this.MarketCap = MarketCap;
        this.RegularMarketVolume = RegularMarketVolume;
        this.Industry = Industry;
        this.Sector = Sector;
        this.LongBusinessSummary = LongBusinessSummary;
        this.FullTimeEmployees = FullTimeEmployees;
        this.Address = Address;
        this.City = City;
        this.Country = Country;

    }
}