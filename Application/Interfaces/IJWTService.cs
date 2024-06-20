using Application.Models.Responses;

namespace Application.Interfaces;

public interface IJWTService
{
    public TokenResponse CreateToken(string id);

    public string? DecodeToken(string token);
}
