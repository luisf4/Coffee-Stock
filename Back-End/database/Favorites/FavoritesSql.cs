using Microsoft.Data.SqlClient;

public class FavoritesSql : Database
{
    public void AddFavorites(string favorite, string user)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "INSERT INTO favorites VALUES (@fav, @user)";
            db.Parameters.AddWithValue("@user", user);
            db.Parameters.AddWithValue("@fav", favorite);
            db.ExecuteNonQuery();
        }
    }

    public void RemoveFavorites(string symbol, string user)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "DELETE FROM favorites where symbol = @symbol and username = @user";
            db.Parameters.AddWithValue("@symbol", symbol);
            db.Parameters.AddWithValue("@user", user);
            db.ExecuteNonQuery();
        }
    }

    public List<string> ReadFavorites(string user) { 
        using (SqlCommand db = new SqlCommand()) { 
            db.Connection = connection;
            db.CommandText = "SELECT * FROM favorites where username = @user";
            db.Parameters.AddWithValue("@user", user);

            SqlDataReader reader = db.ExecuteReader();

            List<String> favorites = new();

            while (reader.Read())
            {
                favorites.Add(reader.GetString(1));
            }
            return favorites;
        }
    }
}