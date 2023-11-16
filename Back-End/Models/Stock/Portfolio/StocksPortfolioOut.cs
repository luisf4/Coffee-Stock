public class StocksPortfolioOut
{
    public int portfolio_stock_id { get; set; }
    public int portfolio_id { get; set; }
    public string stock { get; set; }
    public string name { get; set; }
    public int qnt { get; set; }
    public double price { get; set; }
    public string logo { get; set; }
    public string date { get; set; }

    // Constructor
    public StocksPortfolioOut(int portfolio_stock_id, int portfolio_id, string stock, string name, int qnt, double price, string logo, string date)
    {
        this.portfolio_stock_id = portfolio_stock_id;
        this.portfolio_id = portfolio_id;
        this.stock = stock;
        this.name = name;
        this.qnt = qnt;
        this.price = price;
        this.logo = logo;
        this.date = date;
    }
}
