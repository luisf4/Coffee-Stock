using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class AuthService { 

    public static User user = new User();
    public static User Register(UserDto request) { 
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.Username = request.Username;
        user.PasswordHash = passwordHash;

        // Store user date in the db in the future 
        return user;
    }

    public static User Login(UserDto request) { 
        if (user.Username != request.Username) { 
            return BadRequest("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) {
            return BadRequest("Wrong Password");
        }
        return user;
    }

  private static User BadRequest(string v)
  {
    throw new NotImplementedException();
  }
}