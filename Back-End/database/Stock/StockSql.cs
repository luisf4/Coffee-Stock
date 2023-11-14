
using System.Data;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
public class StockSql : Database
{
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
                        var difference = Math.Abs((timeNow - time).Seconds);
                        Console.WriteLine(difference);
                        // Close the reader before returning
                        reader.Close();

                        // Update the stock if its old data (1h <)
                        if (difference > 30)
                        {
                            Console.WriteLine("old");
                            return "old";
                        }
                        else
                        {
                            Console.WriteLine("Ok");
                            return "ok";
                        }
                    }
                    else
                    {
                        return "No Stock data";
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }

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
                float _price = reader.GetFloat(3);
                string _logo = reader.GetString(4);
                string _requestedAt = reader.GetString(5);
                reader.Close();

                db.CommandText = "SELECT * FROM chartData WHERE symbol = @sym";
                db.Parameters.AddWithValue("@sym", symbol);

                SqlDataReader reader2 = db.ExecuteReader();

                List<HistoricalData> list = new();

                while (reader.Read())
                {
                    HistoricalData chart = new HistoricalData
                    {
                        Date = reader2.GetInt32(3),
                        Open = reader2.GetFloat(4),
                        High = reader2.GetFloat(5),
                        Low = reader2.GetFloat(6),
                        Close = reader2.GetFloat(7),
                        Volume = reader2.GetInt32(8),
                        AdjustedClose = reader2.GetInt32(9)
                    };
                    list.Add(chart);
                }


                if (reader.Read())
                {
                    StockData task = new StockData(
                        symbol: reader.GetString(1),
                        name: reader.GetString(2),
                        price: reader.GetInt32(3),
                        logo: reader.GetString(4),
                        requestedAt: reader.GetString(5),
                        historicalDataPrice: list
                    );
                    reader2.Close();
                    return task;
                }
            }







        }
        return null;
    }
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
