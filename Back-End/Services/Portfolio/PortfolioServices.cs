using Microsoft.Data.SqlClient;

public class PortfolioServices : IPortfolioServices
{
    public Portfolio Create(string portfolio)
    {
        PortoflioSql db = new PortoflioSql();
        db.CreatePortfolio("a");
        throw new NotImplementedException();
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