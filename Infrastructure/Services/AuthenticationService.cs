using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Validators;
using Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class AuthenticationService(IApplicationDbContext _context, IEmailService _emailService, IJWTService _jwtService, IEncryptionService _encryptionService) : IAuthenticationService
{
    public async Task<Result> Login(LoginRequest request)
    {
        var validator = new LoginRequestValidator();
        validator.Validate(request);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email.Trim());
        if (user == null) return Result.Failure("Invalid email or password");
        if (!_encryptionService.Verify(request.Password, user.Password)) return Result.Failure("Incorrect password");
        user.LastLogin = DateTime.Now;
        var token = _jwtService.CreateToken(user.UserId);
        return Result.Success("Login Successful", token);
    }

    public async Task<Result> Register(RegisterRequest request)
    {
        var validator = new RegisterRequestValidator();
        validator.Validate(request);
        var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email || x.UserName == request.UserName);
        if (userExists) return Result.Failure("User with email already exists");
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserId = Guid.NewGuid().ToString(),
            Email = request.Email,
            Password = _encryptionService.Hash(request.Password),
            UserName = request.UserName,
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(new CancellationToken());
        //send email
        return Result.Success("Registration successful", user.UserId);
    }
}