using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Email.Core;

public static class Extensions
{
    public static IServiceCollection AddEmailModuleCollection(this IServiceCollection service)
    {
        service.AddMediatR(Assembly.GetExecutingAssembly());
        return service;
    }
}