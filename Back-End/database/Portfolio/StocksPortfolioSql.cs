using Microsoft.Data.SqlClient;

public class StocksPortfolioSql : Database
{
    public void Create(string user, int id, string stock, int qnt, StockData info)
    {
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "IF EXISTS (SELECT 1 FROM portfolios WHERE portfolio_id = @id AND user_name = @user) " +
                             "BEGIN " +
                             "    INSERT INTO stocks_portfolio VALUES (@id, @stock, @name, @qtd, @price, @logo, @date) " +
                             "END";
            db.Parameters.AddWithValue("@id", id);
            db.Parameters.AddWithValue("@user", user);
            db.Parameters.AddWithValue("@stock", stock);
            db.Parameters.AddWithValue("@name", info.Name);
            db.Parameters.AddWithValue("@qtd", qnt);
            db.Parameters.AddWithValue("@price", info.Price);
            db.Parameters.AddWithValue("@logo", info.Logo);
            db.Parameters.AddWithValue("@date", "date:"); // Assuming this should be replaced with an actual date value
            db.ExecuteNonQuery();
        }
    }

    public List<StocksPortfolioOut> Read(string user, int id)
    {
                using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "SELECT * FROM stocks_portfolio WHERE portfolio_id = @id";
            db.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = db.ExecuteReader();

            List<StocksPortfolioOut> portfolios = new();

            while (reader.Read())
            {
                StocksPortfolioOut portfolio = new StocksPortfolioOut(
                    portfolio_stock_id: reader.GetInt32(0),
                    portfolio_id: reader.GetInt32(1),
                    stock: reader.GetString(2),
                    name: reader.GetString(3),
                    qnt: reader.GetInt32(4),
                    price: reader.GetDouble(5),
                    logo: reader.GetString(6),
                    date: reader.GetString(7)                    
                );
                portfolios.Add(portfolio);
            }
            return portfolios;
        }
    }

    public void Update(string user, int id, string stock, int qnt, string date)
    {
        throw new NotImplementedException();
    }

    public void Delete(string user, int id)
    {
        throw new NotImplementedException();
    }
}