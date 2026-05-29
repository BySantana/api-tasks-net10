using TasksToDo.Models.Base;

namespace TasksToDo.Models;

public class UserModel : BaseEntity
{
    public UserModel(string name, string email, string password)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
        
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));
        
        Name = name;
        Email = email;
        Password = password;
    }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
}