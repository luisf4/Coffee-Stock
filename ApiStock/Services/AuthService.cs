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
}