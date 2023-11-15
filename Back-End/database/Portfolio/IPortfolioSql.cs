public interface IPortfolioSql
{
    public void Create(string portfolio);
    public Portfolio Read(string portfolio);
    public void Update(string portfolio);
    public void Delete(string portfolio);
}