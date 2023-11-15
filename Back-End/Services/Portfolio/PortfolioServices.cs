using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class PortfolioServices : IPortfolioServices
{
    public void Create(string portfolio, string user)
    {
        PortoflioSql db = new PortoflioSql();
        db.CreatePortfolio(portfolio, user);
    }

    public List<Portfolio> ReadAll(string user)
    {
        PortoflioSql db = new PortoflioSql();
        var portfolios = db.ReadMultiplePortfolios(user);
        return portfolios;
    }

    public Portfolio Read(string portfolio)
    {
        throw new NotImplementedException();
    }

    public void Update(string user, int id, string newName)
    {
        PortoflioSql db = new PortoflioSql();
        db.Update(user, id, newName);
    }

    public void Delete(string user, int id)
    {
        PortoflioSql db = new PortoflioSql();
        db.Delete(user, id);
    }
}