
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

public class AuthSql : Database
{

    public string SignUp(User user)
    {
        try
        {


            SqlCommand db = new SqlCommand();
            db.Connection = connection;

            // Check if the username already exists in the database
            db.CommandText = "SELECT COUNT(*) FROM users WHERE username = @user";
            db.Parameters.AddWithValue("@user", user.Username);

            int existingUserCount = (int)db.ExecuteScalar();

            if (existingUserCount > 0)
            {
                // A user with the same username already exists, so return an error
                return "Error: Username already exists.";

            }
            else
            {
                // The username is unique, proceed with the insertion
                db.CommandText = "INSERT INTO users VALUES (@user, @pass, @jwt)";
                db.Parameters.Clear();
                db.Parameters.AddWithValue("@user", user.Username);
                db.Parameters.AddWithValue("@pass", user.PasswordHash);
                db.Parameters.AddWithValue("@jwt", user.Jwt);

                db.ExecuteNonQuery();
                return "ok";
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return "Error: " + ex.Message;
        }
    }


    public string SignIn(UserDto user)
    {
        SqlCommand db = new SqlCommand();
        db.Connection = connection;

        // Define the SQL query to select a user by username
        db.CommandText = "SELECT * FROM users WHERE username = @username";
        db.Parameters.AddWithValue("@username", user.Username);

        // Execute the query
        SqlDataReader reader = db.ExecuteReader();

        // Check if a user with the provided username was found
        if (reader.Read())
        {
            // Retrieve the password hash from the database
            string dbPasswordHash = reader["passwordHash"].ToString();
            Console.WriteLine(dbPasswordHash);
            // Verify the user's password using BCrypt's Verify method
            if (BCrypt.Net.BCrypt.Verify(user.Password, dbPasswordHash))
            {
                return "ok";
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

    // Do this later :)
    // public string CheckJwt(string jwt) { 

    //     SqlCommand db = new SqlCommand();
    //     db.Connection = connection;
    //     db.CommandText = "select * from users where jwt = @jwt";
    //     db.Parameters.AddWithValue("@username", jwt);

    //     return "ok";
    // }


}