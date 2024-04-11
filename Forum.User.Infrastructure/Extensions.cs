using System.Reflection;
using Forum.User.Core.Entities;
using Forum.User.Core.Repository;
using Forum.User.Core.Services;
using Forum.User.Infrastructure.Database;
using Forum.User.Infrastructure.Repository;
using Forum.User.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.User.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddMediatR(Assembly.GetExecutingAssembly());
        service.AddIdentity<ApplicationUser, IdentityRole>(x =>
        {
            x.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        service.AddApplicationDbContext(configuration);
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUserService,UserService>();
        
        return service;
    }
}