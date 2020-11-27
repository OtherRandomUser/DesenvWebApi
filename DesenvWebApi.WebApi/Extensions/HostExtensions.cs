using DesenvWebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesenvWebApi.WebApi.Extensions
{
    public static class HostExtensions
    {
        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
                using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
                    context.Database.Migrate();

            return host;
        }
    }
}