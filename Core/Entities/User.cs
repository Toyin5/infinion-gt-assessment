namespace Core.Entities;

public class User
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string FirstName {get;set;}
    public required string LastName { get; set; }
    public string Email { get; set;}
    public string Password { get; set;}
    public string UserName { get; set;}
    public DateTime LastLogin { get; set;}
}
