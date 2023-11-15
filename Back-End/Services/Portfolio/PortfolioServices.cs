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

    public void Delete(string portfolio)
    {
        throw new NotImplementedException();
    }

    public Portfolio Read(string portfolio)
    {
        throw new NotImplementedException();
    }

    public void Update(string portfolio)
    {
        throw new NotImplementedException();
    }
}