using System.Reflection;
using Forum.User.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.User.Api;

public static class Extensions
{
   public static IServiceCollection AddUserModuleServices(this IServiceCollection service, IConfiguration configuration)
   {
      service.AddInfrastructure(configuration);
    
      return service;
   }

   public static IApplicationBuilder AddUserModule(this IApplicationBuilder app)
   {
      app.UsePathBase("/user");
      return app;
   }
   
}