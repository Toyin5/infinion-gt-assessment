using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Services;
public class JWTService(IConfiguration configuration) : IJWTService
{
    private const int EXPIRATION_MINUTES = 30;

    private readonly IConfiguration _configuration = configuration;

    public TokenResponse CreateToken(string id)
    {
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

        var token = CreateJwtToken(
            CreateClaims(id),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenResponse
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration
        };
    }
    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
       new JwtSecurityToken(
           _configuration["Jwt:Issuer"],
           _configuration["Jwt:Audience"],
           claims,
           expires: expiration,
           signingCredentials: credentials
       );

    private Claim[] CreateClaims(string id)
    {
        return [
                new Claim("Sub", _configuration["Jwt:Subject"]),
                new Claim("Jti", Guid.NewGuid().ToString()),
                new Claim("Iat", DateTime.UtcNow.ToString()),
                new Claim("id", id),
        ];
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var privateKey = File.ReadAllText("private_key.pem");
        var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey);

        return new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256
        );
    }

    public string? DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var claims = jwtSecurityToken.Claims.ToList();
        return claims.First(x => x.Type == "id").Value;
    }
}
