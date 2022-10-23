using Identity.API.Tests.Repository;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Tests.Fixture;

public class CustomApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var desc = services.FirstOrDefault(s => s.ServiceType == typeof(IUserRepository));
            services.Remove(desc);

            services.AddSingleton<IUserRepository, UserMockRepository>();
        });
    }
}