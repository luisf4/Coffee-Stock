using Azure.Identity;

public class Portfolio
{

    public int id { get; set; }
    public string user_name { get; set; }
    public string Name { get; set; }
    public List<PortfolioStock>? Stocks { get; set; }

    public Portfolio(int id, string user_name, string name)
    {
        this.id = id;
        this.user_name = user_name;
        this.Name = name;

    }
    public Portfolio(int id, string user_name, string name, List<PortfolioStock>? stocks)
    {
        this.id = id;
        this.user_name = user_name;
        this.Name = name;
        this.Stocks = stocks;
    }
}

