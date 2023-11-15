using Microsoft.Data.SqlClient;

public class PortoflioSql : Database { 
    public string CreatePortfolio(string portfolio){
        using (SqlCommand db = new SqlCommand())
        {
            // db.Connection = connection;
            // db.CommandText = " INTO portfolio "
        }

        return "";
    }
}