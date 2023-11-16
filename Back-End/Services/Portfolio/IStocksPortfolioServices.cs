using Microsoft.AspNetCore.Mvc;

public interface IStocksPortfolioServices { 
    public  Task Add(StocksPortfolio stock);
    public List<StocksPortfolioOut> Read(string user, int id);
    public void Update(string user, int id, string stock, int qnt);
    public void Delete(string user, int id, string stock);
}