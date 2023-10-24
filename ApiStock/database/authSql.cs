
using Microsoft.Data.SqlClient;

public class StatusSql : Database
{
    public string SingUp(User user)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;

        cmd.CommandText = "INSERT INTO users VALUES (@user,@pass)";

        cmd.Parameters.AddWithValue("@user", user.Username);
        cmd.Parameters.AddWithValue("@pass", user.PasswordHash);

        cmd.ExecuteNonQuery();
        return "ok";
    }


public string SignIn(User user)
{

    SqlCommand cmd = new SqlCommand();
    cmd.Connection = connection;

    // Define the SQL query to select a user by username
    cmd.CommandText = "SELECT * FROM users WHERE username = @username";
    cmd.Parameters.AddWithValue("@username", user.Username);

    // Execute the query
    SqlDataReader reader = cmd.ExecuteReader();

    // Check if a user with the provided username was found
    if (reader.Read())
    {
        // Retrieve the password from the database
        string dbPassword = reader["passwordHash"].ToString();

        // Verify the user's password
        if (user.PasswordHash == dbPassword)
        {
            return "Authentication successful";
        }
        else
        {
            return "Incorrect password";
        }
    }
    else
    {
        return "User not found";
    }
}

}