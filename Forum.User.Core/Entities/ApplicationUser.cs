using Microsoft.AspNetCore.Identity;

namespace Forum.User.Core.Entities;

public class ApplicationUser:IdentityUser
{
    public bool IsBlocked { get; private set; }
    public ApplicationUser(string email)
    {
        Email = email;
        NormalizedEmail = email.ToUpper();
    }

    public void BlockUser()
    {
        IsBlocked = true;
    }
}