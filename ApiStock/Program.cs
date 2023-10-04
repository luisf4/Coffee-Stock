var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hellow!");


app.MapGet("/stock/{symbol}", (string symbol) => { 
    StockServices stock = new StockServices();
    return stock.GetStockInfo(symbol);
});

// Gets stock sharts
app.MapGet("/stock/{symbol}/sharts/{date}", (string symbol, string date) => {
    StockServices stock = new StockServices();
    return stock.GetStockShart("AAPL",date);
});



app.Run();
