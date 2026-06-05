using TasksToDo.Domain.Models.Base;

namespace TasksToDo.Domain.Models;

public class UserModel : BaseEntity
{
    public UserModel(string email, string password)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
        
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));
        
        Email = email;
        Password = password;
    }
    
    public string Email { get; private set; }
    public string Password { get; private set; }
}