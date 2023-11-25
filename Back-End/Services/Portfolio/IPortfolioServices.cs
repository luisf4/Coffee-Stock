using Microsoft.AspNetCore.Mvc;

public interface IPortfolioServices { 
    public void Create(string portfolio, string user);
    public List<Portfolio> ReadAll(string user);
    public Portfolio Read(string portfolio);
    public void Update(string user, int id, string newName);
    public void Delete(string user, int id);
    
}