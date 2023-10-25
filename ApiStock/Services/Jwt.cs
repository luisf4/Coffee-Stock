using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtServices { 
        public string CreateToken(UserDto user) { 
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