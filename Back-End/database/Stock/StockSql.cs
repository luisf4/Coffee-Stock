
using System.Data;
using System.Globalization;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Data.SqlClient;
public class StockSql : Database
{

    // Checks if the stock data is old
    public string CheckTimeData(string symbol)
    {
        try
        {
            using (SqlCommand db = new SqlCommand())
            {
                db.Connection = connection;

                db.CommandText = "SELECT * FROM stocks WHERE symbol = @symbol";
                db.Parameters.AddWithValue("@symbol", symbol);

                using (SqlDataReader reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // calculates the time is 1 <
                        var time = DateTime.Parse(reader.GetString(5));
                        var timeNow = DateTime.Now;
                        var difference = Math.Abs((timeNow - time).Minutes);
                        Console.WriteLine("The stock " + symbol + " was updated " + difference + "mn ago!");
                        // Close the reader before returning
                        reader.Close();

                        // Update the stock if its old data (1h <)
                        if (difference > 1)
                        {
                            return "old";
                        }
                        else
                        {
                            return "ok";
                        }
                    }
                    else
                    {
                        return "No Stock data";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }

    // Read stock data and charts 
    public StockData ReadStock(string symbol)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;

            db.CommandText = "SELECT * FROM stocks WHERE symbol = @sym";
            db.Parameters.AddWithValue("@sym", symbol);

            SqlDataReader reader = db.ExecuteReader();
            if (reader.Read())
            {
                string _symbol = reader.GetString(1);
                string _name = reader.GetString(2);
                double _price = reader.GetDouble(3);
                string _logo = reader.GetString(4);
                string _requestedAt = reader.GetString(5);
                decimal _MarketCap = reader.GetDecimal(6);
                decimal _RegularMarketVolume = reader.GetDecimal(7);
                string _industry = reader.GetString(8);
                string _sector = reader.GetString(9);
                string _longBusinessSummary = reader.GetString(10);
                decimal _fullTimeEmployees = reader.GetDecimal(11);
                string _address1 = reader.GetString(12);
                string _city = reader.GetString(13);
                string _country = reader.GetString(14);

                reader.Close();

                db.CommandText = "SELECT * FROM chartData WHERE symbol = @sym";

                SqlDataReader reader2 = db.ExecuteReader();

                List<HistoricalDataPrice> list = new();

                while (reader2.Read())
                {
                    HistoricalDataPrice chart = new HistoricalDataPrice
                    {
                        // Date = 1,
                        Date = reader2.GetInt32(2),
                        Open = reader2.GetDouble(3),
                        High = reader2.GetDouble(4),
                        Low = reader2.GetDouble(5),
                        Close = reader2.GetDouble(6),
                        Volume = reader2.GetInt32(7),
                        AdjustedClose = reader2.GetDouble(8)
                    };
                    list.Add(chart);
                }

                reader2.Close();

                db.CommandText = "SELECT * FROM chartDividendsData WHERE symbol = @sym";
                db.Parameters.AddWithValue("@symbo", symbol);

                SqlDataReader reader3 = db.ExecuteReader();

                List<CashDividends> listDiv = new();

                while (reader3.Read())
                {
                    CashDividends chart = new CashDividends
                    {
                        AssetIssued = reader3.GetString(1),
                        PaymentDate = reader3.GetString(2),
                        Rate = reader3.GetDouble(3),
                        RelatedTo = reader3.GetString(4),
                    };
                    listDiv.Add(chart);

                }

                StockData task = new StockData(
                    symbol: _symbol,
                    name: _name,
                    price: _price,
                    logo: _logo,
                    requestedAt: _requestedAt,
                    MarketCap: _MarketCap,
                    RegularMarketVolume: _RegularMarketVolume,
                    Industry: _industry,
                    Sector: _sector,
                    LongBusinessSummary: _longBusinessSummary,
                    FullTimeEmployees: _fullTimeEmployees,
                    Address: _address1,
                    City: _city,
                    Country: _country,
                    historicalDataPrice: list,
                    DividendsData: listDiv
                );
                reader3.Close();
                return task;

            }
        }
        return null!;
    }

    // Write stocks to the db
    public string WriteStock(StockData stock)
    {
        try
        {
            using (SqlCommand db = new SqlCommand())
            {
                db.Connection = connection;
                // Creates a new stock 
                db.CommandText = "INSERT INTO stocks (symbol, [name], price, logo, requestedAt, marketcap, regularMarketVolume," +
                "industry, sector, longBusinessSummary, fullTimeEmployees, [address], city, country) " +
                "VALUES (@symbol, @name, @price, @logo, @req, @marketcap, @volume, @industry, @sector, @summary, @employees, @adress, @city, @country)";
                db.Parameters.AddWithValue("@symbol", stock.Symbol);
                db.Parameters.AddWithValue("@name", stock.Name);
                db.Parameters.AddWithValue("@price", stock.Price);
                db.Parameters.AddWithValue("@logo", stock.Logo);
                db.Parameters.AddWithValue("@req", stock.RequestedAt);

                // new
                db.Parameters.AddWithValue("@marketcap", stock.MarketCap);
                db.Parameters.AddWithValue("@volume", stock.RegularMarketVolume);
                db.Parameters.AddWithValue("@industry", stock.Industry);
                db.Parameters.AddWithValue("@sector", stock.Sector);
                db.Parameters.AddWithValue("@summary", stock.LongBusinessSummary);
                db.Parameters.AddWithValue("@employees", stock.FullTimeEmployees);
                db.Parameters.AddWithValue("@adress", stock.Address);
                db.Parameters.AddWithValue("@city", stock.City);
                db.Parameters.AddWithValue("@country", stock.Country);

                db.ExecuteNonQuery();

                db.Parameters.Clear();

                // Perform operations with the ID, such as inserting into chartData
                foreach (var historicalData in stock.HistoricalDataPrice)
                {
                    using (SqlCommand insertCommand = new SqlCommand())
                    {
                        insertCommand.Connection = connection;
                        insertCommand.CommandText = "INSERT INTO chartData (symbol, [date], [open], [high], [low], [close], volume, adjustedClose, requestedAt) VALUES (@symbol, @date, @open, @high, @low, @close, @volume, @adjustedClose, @requestedAt)";
                        insertCommand.Parameters.AddWithValue("@symbol", stock.Symbol);
                        insertCommand.Parameters.AddWithValue("@date", historicalData.Date);
                        insertCommand.Parameters.AddWithValue("@open", historicalData.Open);
                        insertCommand.Parameters.AddWithValue("@high", historicalData.High);
                        insertCommand.Parameters.AddWithValue("@low", historicalData.Low);
                        insertCommand.Parameters.AddWithValue("@close", historicalData.Close);
                        insertCommand.Parameters.AddWithValue("@volume", historicalData.Volume);
                        insertCommand.Parameters.AddWithValue("@adjustedClose", historicalData.AdjustedClose);
                        insertCommand.Parameters.AddWithValue("@requestedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        insertCommand.ExecuteNonQuery();
                    }
                }

                foreach (var historicalData in stock.DividendsData)
                {
                    using (SqlCommand insertCommand = new SqlCommand())
                    {
                        insertCommand.Connection = connection;
                        insertCommand.CommandText = "INSERT INTO chartDividendsData (symbol, paymentDate, rate, relatedTo, requestedAt) VALUES (@symbol, @paymentDate, @rate, @relatedTo, @requestedAt)";
                        insertCommand.Parameters.AddWithValue("@symbol", stock.Symbol);
                        insertCommand.Parameters.AddWithValue("@paymentDate", historicalData.PaymentDate);
                        insertCommand.Parameters.AddWithValue("@rate", historicalData.Rate);
                        insertCommand.Parameters.AddWithValue("@relatedTo", historicalData.RelatedTo);
                        insertCommand.Parameters.AddWithValue("@requestedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        insertCommand.ExecuteNonQuery();
                        db.Parameters.Clear();
                    }
                }
                return $"Stock: {stock.Symbol} created.";
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }

    // Update stocks data in the db
    public string UpdateStock(StockData stock)
    {
        try
        {
            using (SqlCommand db = new SqlCommand())
            {
                db.Connection = connection;
                db.CommandText = "UPDATE stocks SET " +
                    "symbol = @symbol, [name] = @name, price = @price, logo = @logo, requestedAt = @req, " +
                    "marketcap = @marketcap, regularMarketVolume = @volume, industry = @industry, " +
                    "sector = @sector, longBusinessSummary = @summary, fullTimeEmployees = @employees, " +
                    "[address] = @adress, city = @city, country = @country WHERE symbol = @symbol";

                // Add parameters
                db.Parameters.AddWithValue("@symbol", stock.Symbol);
                db.Parameters.AddWithValue("@name", stock.Name);
                db.Parameters.AddWithValue("@price", stock.Price);
                db.Parameters.AddWithValue("@logo", stock.Logo);
                db.Parameters.AddWithValue("@req", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.Parameters.AddWithValue("@marketcap", stock.MarketCap);
                db.Parameters.AddWithValue("@volume", stock.RegularMarketVolume);
                db.Parameters.AddWithValue("@industry", stock.Industry);
                db.Parameters.AddWithValue("@sector", stock.Sector);
                db.Parameters.AddWithValue("@summary", stock.LongBusinessSummary);
                db.Parameters.AddWithValue("@employees", stock.FullTimeEmployees);
                db.Parameters.AddWithValue("@adress", stock.Address);
                db.Parameters.AddWithValue("@city", stock.City);
                db.Parameters.AddWithValue("@country", stock.Country);

                // Update the existing stock
                db.ExecuteNonQuery();
                Console.WriteLine("Successfully updated the stock!");

                db.CommandText = "SELECT TOP 1 * FROM chartData ORDER BY chart_id DESC;";

                using (SqlDataReader reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int lastReq = int.Parse(reader.GetString(1));

                        foreach (var historicalData in stock.HistoricalDataPrice)
                        {
                            if (historicalData.Date > lastReq)
                                using (SqlCommand insertCommand = new SqlCommand())
                                {
                                    insertCommand.Connection = connection;
                                    insertCommand.CommandText = "INSERT INTO chartData (symbol, [date], [open], [high], [low], [close], volume, adjustedClose, requestedAt) VALUES (@symbol, @date, @open, @high, @low, @close, @volume, @adjustedClose, @requestedAt)";
                                    insertCommand.Parameters.AddWithValue("@symbol", stock.Symbol);
                                    insertCommand.Parameters.AddWithValue("@date", historicalData.Date);
                                    insertCommand.Parameters.AddWithValue("@open", historicalData.Open);
                                    insertCommand.Parameters.AddWithValue("@high", historicalData.High);
                                    insertCommand.Parameters.AddWithValue("@low", historicalData.Low);
                                    insertCommand.Parameters.AddWithValue("@close", historicalData.Close);
                                    insertCommand.Parameters.AddWithValue("@volume", historicalData.Volume);
                                    insertCommand.Parameters.AddWithValue("@adjustedClose", historicalData.AdjustedClose);
                                    insertCommand.Parameters.AddWithValue("@requestedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    insertCommand.ExecuteNonQuery();
                                }
                        }
                    }
                    reader.Close();

                }

                db.CommandText = "SELECT TOP 1 * FROM chartDividendsData ORDER BY chart_id DESC;";

                using (SqlDataReader reader2 = db.ExecuteReader())
                {
                    if (reader2.Read())
                    {
                        // Get the old date 
                        string lastReq = reader2.GetString(2);

                        // Convert the date
                        DateTime lastReqParsed = DateTime.ParseExact(lastReq, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                        foreach (var historicalData in stock.DividendsData)
                        {
                            // Convert the date
                            DateTime newReq = DateTime.ParseExact(historicalData.PaymentDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                            // If the newdate is after the old one it will add to the db as a new dividend payment 
                            if (newReq > lastReqParsed)
                                using (SqlCommand insertCommand = new SqlCommand())
                                {
                                    insertCommand.Connection = connection;
                                    insertCommand.CommandText = "INSERT INTO chartDividendsData (symbol, paymentDate, rate, relatedTo, requestedAt) VALUES (@symbol, @paymentDate, @rate, @relatedTo, @requestedAt)";
                                    insertCommand.Parameters.AddWithValue("@symbol", stock.Symbol);
                                    insertCommand.Parameters.AddWithValue("@paymentDate", historicalData.PaymentDate);
                                    insertCommand.Parameters.AddWithValue("@rate", historicalData.Rate);
                                    insertCommand.Parameters.AddWithValue("@relatedTo", historicalData.RelatedTo);
                                    insertCommand.Parameters.AddWithValue("@requestedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    insertCommand.ExecuteNonQuery();
                                    db.Parameters.Clear();
                                }
                            else
                            {
                                Console.WriteLine("no new data");
                            }
                        }
                    }
                    reader2.Close();
                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }
}
