
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

public class StockSql : Database
{

    public string Write(List<string> stock)
    {
        try
        {

            SqlCommand db = new SqlCommand();
            db.Connection = connection;

            // The username is unique, proceed with the insertion
            db.CommandText = "insert into stocks (symbol,[name],price,logo,requestedAt) values(@symbol, @name, @price, @logo, @date);";
            db.Parameters.Clear();
            db.Parameters.AddWithValue("@symbol", stock);
            db.Parameters.AddWithValue("@name", stock);
            db.Parameters.AddWithValue("@price", stock);
            db.Parameters.AddWithValue("@logo", stock);
            db.Parameters.AddWithValue("@date", stock);


            db.ExecuteNonQuery();
            return "ok";

        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }


    // public string Read()
    // {
    //     SqlCommand db = new SqlCommand();
    //     db.Connection = connection;

    //     // Define the SQL query to select a user by username
    //     db.CommandText = "SELECT * FROM stocks";

    //     // Execute the query
    //     SqlDataReader reader = db.ExecuteReader();

    // }



}