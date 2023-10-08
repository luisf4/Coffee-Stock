using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

public class AuthService
{

    public static User user = new User();
    public static User Register(UserDto request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.Username = request.Username;
        user.PasswordHash = passwordHash;

        // Store user date in the db in the future 
        return user;
    }

    public static string Login(UserDto request)
    {
        if (user.Username != request.Username)
        {
            return "User not found";
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return "Wrong Password";
        }

        string token = CreateToken(user);

        // return token;
        return token;
    }

    public static string CreateToken(User user) { 
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username)
        };

        // Change it later >:
        byte[] superupeerSecretKey = Encoding.UTF8.GetBytes("Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa");
        var key = new SymmetricSecurityKey(superupeerSecretKey);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
}