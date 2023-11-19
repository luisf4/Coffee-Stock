
using System.Data;
using System.Reflection.Metadata;
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
                        Console.WriteLine("The stock "+symbol+" was updated "+ difference+ "mn ago!");
                        // Close the reader before returning
                        reader.Close();

                        // Update the stock if its old data (1h <)
                        if (difference > 30)
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

                reader.Close();

                db.CommandText = "SELECT * FROM chartData WHERE symbol = @sym";
                db.Parameters.AddWithValue("@symbo", symbol);

                SqlDataReader reader2 = db.ExecuteReader();

                List<HistoricalData> list = new();

                while (reader2.Read())
                {
                    HistoricalData chart = new HistoricalData
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

                StockData task = new StockData(
                    symbol: _symbol,
                    name: _name,
                    price: _price,
                    logo: _logo,
                    requestedAt: _requestedAt,
                    historicalDataPrice: list
                );
                reader2.Close();
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

                db.CommandText = "SELECT * FROM stocks WHERE symbol = @sy";
                db.Parameters.AddWithValue("@sy", stock.Symbol);

                using (SqlDataReader reader = db.ExecuteReader())
                {
                    // Close the reader before executing another query
                    reader.Close();

                    // Creates a new stock 
                    db.CommandText = "INSERT INTO stocks (symbol, [name], price, logo, requestedAt) VALUES (@symbol, @name, @price, @logo, @req)";
                    db.Parameters.Clear();
                    db.Parameters.AddWithValue("@symbol", stock.Symbol);
                    db.Parameters.AddWithValue("@name", stock.Name);
                    db.Parameters.AddWithValue("@price", stock.Price);
                    db.Parameters.AddWithValue("@logo", stock.Logo);
                    db.Parameters.AddWithValue("@req", stock.RequestedAt);

                    db.ExecuteNonQuery();
                }
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
                // Update the existing stock
                db.CommandText = "UPDATE stocks SET symbol = @symbol, [name] = @name, price = @price, logo = @logo, requestedAt = @req WHERE symbol = @symbol";
                db.Parameters.AddWithValue("@symbol", stock.Symbol);
                db.Parameters.AddWithValue("@name", stock.Name);
                db.Parameters.AddWithValue("@price", stock.Price);
                db.Parameters.AddWithValue("@logo", stock.Logo);
                db.Parameters.AddWithValue("@req", stock.RequestedAt);

                db.ExecuteNonQuery();

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
