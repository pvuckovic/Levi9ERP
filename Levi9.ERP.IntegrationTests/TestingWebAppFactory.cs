using Levi9.ERP.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Levi9.ERP.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        private DataBaseContext _dataBaseContext;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<DataBaseContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                var serviceProvider = services.BuildServiceProvider();

                _dataBaseContext = serviceProvider.GetRequiredService<DataBaseContext>();
                _dataBaseContext.Database.EnsureCreated();
            });
        }

    }
}
