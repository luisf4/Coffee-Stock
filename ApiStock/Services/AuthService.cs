using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;

public class AuthService
{

    public static string Register(UserDto request)
    {
        AuthSql db = new AuthSql();
        JwtServices jwt = new JwtServices();

        // Hash the password
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Creates a Jwt
        string token = jwt.CreateToken(request);

        User user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Jwt = token
        };

        // Store user
        var res = db.SignUp(user);

        // See if the user was created 
        if (res == "ok")
        {
            return token;
        }
        else
        {
            return res;
        }
    }

    public static string Login(UserDto request)
    {
        AuthSql db = new AuthSql();
        JwtServices jwt = new JwtServices();
        string res = db.SignIn(request);
        string token = jwt.CreateToken(request);

        Console.WriteLine(res);

        // See if the user was logged 
        if (res == "ok")
        {
            return token;
        }
        else
        {
            return res;
        }
    }

    public static string VerifyJWT(string jwt)
    {
        JwtServices check = new JwtServices();
 
        var res = check.VerifyToken(jwt);

        return res;
    }

}