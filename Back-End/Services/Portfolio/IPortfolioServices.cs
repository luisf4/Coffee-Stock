using Microsoft.AspNetCore.Mvc;

public interface IPortfolioServices { 
    public void Create(string portfolio, string token);
    public Portfolio Read(string portfolio);
    public void Update(string portfolio);
    public void Delete(string portfolio);
    
}