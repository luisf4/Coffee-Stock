
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

public class StocksPortfolioServices : IStocksPortfolioServices

{
    public async Task Add(StocksPortfolio stock)
    {
        StockServices stockServices = new StockServices();
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        StockData res = await stockServices.GetStockInfo(stock.stock, "5d");
        stocksPortfolioSql.Create(stock, res);
    }


    public List<StocksPortfolioOut> Read(string user, int id)
    {
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        var res = stocksPortfolioSql.Read(user, id);
        return res;
    }

    public void Update(StocksPortfolio stocks, int id)
    {
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        stocksPortfolioSql.Update(stocks, id);
    }

    public void Delete(string user, int id, int portfolio_id)
    {
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        stocksPortfolioSql.Delete(user, id, portfolio_id);
    }
}