
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

public class StocksPortfolioServices : IStocksPortfolioServices

{
    public async Task Add(StocksPortfolio stock)
    {
        StockServices stockServices = new StockServices();
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        StockData res = await stockServices.GetStockInfo(stock.stock, "5d");
        stocksPortfolioSql.Create(stock.user, stock.portfolio_id, stock.stock, stock.qnt, res);
    }


    public List<StocksPortfolioOut> Read(string user, int id)
    {
        StocksPortfolioSql stocksPortfolioSql = new StocksPortfolioSql();
        var res = stocksPortfolioSql.Read(user, id);
        return res;
    }

    public void Update(string user, int id, string stock, int qnt)
    {
        throw new NotImplementedException();
    }

    public void Delete(string user, int id, string stock)
    {
        throw new NotImplementedException();
    }
}