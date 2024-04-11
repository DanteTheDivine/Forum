using Forum.User.Core.Entities;
using Forum.User.Core.Repository;
using Forum.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Forum.User.Infrastructure.Repository;

internal class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task AddAsync(ApplicationUser user)
    {
        await _applicationDbContext.ApplicationUsers.AddAsync(user);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
         _applicationDbContext.ApplicationUsers.Update(user);
        await _applicationDbContext.SaveChangesAsync();
    }

    public  Task<ApplicationUser> GetByEmail(string email)
    {
       return _applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(x=>x.Email==email);
    }

    public  Task<ApplicationUser> GetById(string Id)
    {
        return _applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(x=>x.Id==Id);
    }
}