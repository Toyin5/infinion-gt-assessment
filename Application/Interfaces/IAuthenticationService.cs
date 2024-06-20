using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;
public interface IAuthenticationService
{
    Task<Result> Register(RegisterRequest request);
    Task<Result> Login(LoginRequest request);
}
