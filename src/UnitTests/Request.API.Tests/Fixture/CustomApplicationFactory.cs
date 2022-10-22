using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Request.API.Tests.Repository;
using Request.Application.Contracts;

namespace Request.API.Tests.Fixture;

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