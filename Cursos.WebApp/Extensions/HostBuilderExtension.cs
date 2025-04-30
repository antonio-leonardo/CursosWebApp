namespace Cursos.WebApp.Extensions
{
    public static class HostBuilderExtension
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            host.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddEnvironmentVariables();
            });
            return host;
        }
    }
}
