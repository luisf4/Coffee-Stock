using Microsoft.AspNetCore.Mvc;

public interface IStocksPortfolioServices { 
    public  Task Add(StocksPortfolio stock);
    public List<StocksPortfolioOut> Read(string user, int id);
    public void Update(StocksPortfolio stocks, int id);
    public void Delete(string user, int id, int portfolio_id);
}