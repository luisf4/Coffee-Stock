using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection connection;

    public Database()
    {
        connection = new SqlConnection("Server=localhost,1433; Database=coffeestock; User Id=sa; Password=1q2w3e4r@#$; TrustServerCertificate=true;");
        connection.Open();
    }

    public void Dispose()
    {
        connection.Close();
    }
}