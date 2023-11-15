using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtServices
{

    string secretKey = "Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa Banana, bananaaaaaaaaaaa";


    public string CreateToken(UserDto user)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username),
        };

        // Change it later >:
        byte[] superupeerSecretKey = Encoding.UTF8.GetBytes(secretKey);
        var key = new SymmetricSecurityKey(superupeerSecretKey);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public string VerifyToken(string jwt)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                // Specify your validation rules
                ValidateIssuer = false, // Set to true if you want to validate the issuer
                ValidateAudience = false, // Set to true if you want to validate the audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // Use your secret key here
            };

            // Try to validate the token
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);

            // Check if the token has expired
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                {
                    return "Token has expired";
                }
            }
            return "ok";
        }
        catch (SecurityTokenException)
        {
            return "Token is invalid";
        }
        catch (Exception ex)
        {
            return "An error occurred: " + ex.Message;
        }
    }

    public string GetUsername(string jwt)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                // Specify your validation rules
                ValidateIssuer = false, // Set to true if you want to validate the issuer
                ValidateAudience = false, // Set to true if you want to validate the audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // Use your secret key here
            };

            // Try to validate the token
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);

            // Check if the token has expired
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                {
                    return "Token has expired";
                }
            }
            // At this point, the token is valid, and you can access its claims
            string username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

            // Check if the username claim exists
            if (!string.IsNullOrEmpty(username))
            {
                return username;
            }
            else
            {
                return "Username claim not found";
            }
        }
        catch (SecurityTokenException)
        {
            return "Token is invalid";
        }
        catch (Exception ex)
        {
            return "An error occurred: " + ex.Message;
        }
    }
}