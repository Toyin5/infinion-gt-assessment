namespace Application.Interfaces;
public interface IEmailService
{
    Task SendSuccessMail(string email, string name);
}