using Microsoft.Data.SqlClient;

public class PortoflioSql : Database
{
    public void CreatePortfolio(string portfolio, string user)
    {

        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "INSERT INTO portfolios VALUES (@user, @name)";
            db.Parameters.AddWithValue("@user", user);
            db.Parameters.AddWithValue("@name", portfolio);
            db.ExecuteNonQuery();
        }
    }

    public List<Portfolio> ReadMultiplePortfolios(string user)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "SELECT * FROM portfolios WHERE user_name = @user_name";
            db.Parameters.AddWithValue("@user_name", user);

            SqlDataReader reader = db.ExecuteReader();

            List<Portfolio> portfolios = new();

            while (reader.Read())
            {
                Portfolio portfolio = new Portfolio(
                    id: reader.GetInt32(0),
                    user_name: reader.GetString(1),
                    name: reader.GetString(2)
                );
                portfolios.Add(portfolio);
            }
            return portfolios;
        }
    }
    public void Update(string user, int id, string newName)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            // Update the existing portfolio name for a specific user and ID
            db.CommandText = "UPDATE portfolios SET [name] = @newName WHERE portfolio_id = @id AND user_name = @user";
            db.Parameters.AddWithValue("@id", id);
            db.Parameters.AddWithValue("@user", user);
            db.Parameters.AddWithValue("@newName", newName);
            db.ExecuteNonQuery();
        }
    }

public void Delete(string user, int id)
{
    using (SqlCommand db = new SqlCommand())
    {
        db.Connection = connection;

        // Delete from stocks_portfolio
        db.CommandText = "DELETE FROM stocks_portfolio WHERE portfolio_id = @id";
        db.Parameters.AddWithValue("@id", id);
        db.ExecuteNonQuery();

        // Clear parameters before reusing the SqlCommand for a new query
        db.Parameters.Clear();

        // Delete from portfolios
        db.CommandText = "DELETE FROM portfolios WHERE portfolio_id = @id AND user_name = @user";
        db.Parameters.AddWithValue("@id", id);
        db.Parameters.AddWithValue("@user", user);
        db.ExecuteNonQuery();
    }
}

}