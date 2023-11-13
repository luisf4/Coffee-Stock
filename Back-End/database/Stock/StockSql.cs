
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

public class StockSql : Database
{

    public string CheckTimeData(string symbol)
    {
        try
        {

            SqlCommand db = new SqlCommand();
            db.Connection = connection;

            db.CommandText = "SELECT * FROM stocks WHERE symbol = @symbol";
            db.Parameters.AddWithValue("@symbol", symbol);

            SqlDataReader reader = db.ExecuteReader();

            if (reader.Read())
            {
                // calculates the time is 1 <
                var time = DateTime.Parse(reader.GetString(5));
                var timeNow = DateTime.Now;
                var difference = Math.Abs((timeNow - time).Hours);

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
        try
        {

            SqlCommand db = new SqlCommand();
            db.Connection = connection;

            db.CommandText = "SELECT * FROM stocks WHERE symbol = @symbol";
            db.Parameters.AddWithValue("@symbol", symbol);

            SqlDataReader reader = db.ExecuteReader();

            if (reader.Read())
            {
                
            }
            return null;

        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }

    public string WriteStocks(StockData stock)
    {
        try
        {
            SqlCommand db = new SqlCommand();
            db.Connection = connection;

            db.CommandText = "SELECT * FROM stocks WHERE symbol = @symbol";
            db.Parameters.AddWithValue("@symbol", stock.Symbol);

            SqlDataReader reader = db.ExecuteReader();

            if (reader.Read())
            {
                // calculates the time is 1 <
                var time = DateTime.Parse(reader.GetString(5));
                var timeNow = DateTime.Now;
                var difference = Math.Abs((timeNow - time).Hours);

                // Update the stock if its old data (1h <)
                if (difference > 1)
                {
                    db.CommandText = "UPDATE stocks SET symbol = @symbol, [name] = @name, price = @price, logo = @logo, requestedAt = @req WHERE symbol = @symbol";
                    db.Parameters.AddWithValue("@symbol", stock.Symbol);
                    db.Parameters.AddWithValue("@name", stock.Name);
                    db.Parameters.AddWithValue("@price", stock.Price);
                    db.Parameters.AddWithValue("@logo", stock.Logo);
                    db.Parameters.AddWithValue("@req", stock.RequestedAt);

                    db.ExecuteNonQuery();
                }
            }
            else
            {
                // Creates a new stock 
                db.CommandText = "insert into stocks (symbol, [name], price, logo, requestedAt) values(@symbol, @name, @price, @logo, @req);";
                db.Parameters.Clear();
                db.Parameters.AddWithValue("@symbol", stock.Symbol);
                db.Parameters.AddWithValue("@name", stock.Name);
                db.Parameters.AddWithValue("@price", stock.Price);
                db.Parameters.AddWithValue("@logo", stock.Logo);
                db.Parameters.AddWithValue("@req", stock.RequestedAt);

                db.ExecuteNonQuery();
                return $"Stock: {stock.Symbol} created.";
            }
            return null!;

        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }


    // public string ReadStocks()
    // {
    //     SqlCommand db = new SqlCommand();
    //     db.Connection = connection;

    //     // Define the SQL query to select a user by username
    //     db.CommandText = "SELECT * FROM stocks";

    //     // Execute the query
    //     SqlDataReader reader = db.ExecuteReader();

    // }



}