using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Cursos.WebApp.IntegrationTest
{
    public class BaseTest
    {
        protected readonly TestWebApplicationFactory _factory;
        public BaseTest(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }
    }

    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
        }
    }
}