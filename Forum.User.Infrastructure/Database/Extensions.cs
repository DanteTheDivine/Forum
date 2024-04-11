using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.User.Infrastructure.Database;

internal static class Extensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>(
            opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
        
        
        return service;
    }
}