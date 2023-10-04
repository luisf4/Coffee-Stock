var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/stock/{symbol}", (string symbol) => { 
    StockServices stock = new StockServices();
    return stock.GetStockBySymbol(symbol);
});

app.MapGet("/stock/{symbol}/sharts/{date}", (string symbol, string date) => {
    StockServices stock = new StockServices();
    return stock.GetStockShart("AAPL",date);
});
app.Run();
