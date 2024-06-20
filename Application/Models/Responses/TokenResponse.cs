namespace Application.Models.Responses;

public class TokenResponse
{
   
    public required string Token { get; set; }
    public DateTime Expiration { get; set; }
}
