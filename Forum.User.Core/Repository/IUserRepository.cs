using Forum.User.Core.Entities;

namespace Forum.User.Core.Repository;

public interface IUserRepository
{
    Task AddAsync(ApplicationUser user);
    Task UpdateAsync(ApplicationUser user);
    Task<ApplicationUser> GetByEmail(string email);
    Task<ApplicationUser> GetById(string Id);
}