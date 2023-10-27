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
            new Claim(ClaimTypes.Name, user.Username)
        };

        // Change it later >:
        byte[] superupeerSecretKey = Encoding.UTF8.GetBytes(secretKey);
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


    public string VerifyToken(string jwt, string secretKey)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Set this to true if you want to validate the issuer
                ValidateAudience = false, // Set this to true if you want to validate the audience
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey)), // Use your secret key here
            };


            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);

            // At this point, the token is valid, and you can access its claims
            // Example: claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            // USE THIS TO VERIFY IF THE USER CAN CHANGE HIS PORTFOLIO AND STOCKS IN THE PORTFOLIO
            // IF CLAINS.NAME = USER NAME .....


            // Token is valid
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
}