using Identity.API.Tests.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;

namespace Request.API.Tests;

public class CustomApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var desc = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestRepository));
            services.Remove(desc);

            services.AddSingleton<IRequestRepository, RequestMockRepository>();
        });
    }
}