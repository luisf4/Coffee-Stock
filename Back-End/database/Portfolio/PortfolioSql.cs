using Microsoft.Data.SqlClient;

public class PortoflioSql : Database { 
    public void CreatePortfolio(string portfolio, string user){
        
        using (SqlCommand db = new SqlCommand())
        {
            db.Connection = connection;
            db.CommandText = "INSERT INTO portfolios VALUES (@user, @name)";
            db.Parameters.AddWithValue("@user", user);
            db.Parameters.AddWithValue("@name", portfolio);
            db.ExecuteNonQuery();
        }
    }

    // public List<Portfolio> ReadMultiplePortfolios(string user) { 
    //     using (SqlCommand db = new SqlCommand()) { 
    //         db.Connection = connection;
    //         db.CommandText= "SELECT * FROM portfolios WHERE user_name = @user_name";
    //         db.Parameters.AddWithValue("@user_name", user);

    //         SqlDataReader reader = db.ExecuteReader();

    //         while(reader.Read()) { 
                
    //         }
    //     }
    // }
}