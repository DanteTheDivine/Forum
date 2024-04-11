using System.Security.Claims;
using Forum.User.Core.Entities;

namespace Forum.User.Core.Services;

public interface IUserService
{
    Task<ForumUserServiceResult<ClaimsPrincipal>> SignInUser(string user, string password);
    Task BlockUser(string user);
    Task<ForumUserServiceResult<ClaimsPrincipal>> SingUpUser(string userName, string password);
    Task ConfirmEmail(string token, string email);
    Task<ForumUserServiceResult<string>> GetUserConfirmationCode(string email);

}

public record ForumUserServiceResult<T>
{
    public bool IsSuccess { get; set; } = true;
    public string SucessMessage { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public T Data { get; set; }
    public static ForumUserServiceResult<T> ReturnError(IEnumerable<string> errors)
    => new ForumUserServiceResult<T>() { Errors = errors.ToList(),IsSuccess = false};
}