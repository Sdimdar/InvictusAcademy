namespace SessionGatewayService.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureSessionServices(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.Name = ".InvictusAcademy.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(3600);
            options.Cookie.IsEssential = true;
        });
        return services;
    }
}
