using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace AareonTechnicalTest.Tests
{
    public class TestServerFixture : IDisposable, IAsyncLifetime
    {
        private IHost host;
        public HttpClient Client { get; private set; }
       
        public IServiceProvider ServiceProvider { get; private set; }

        public void Dispose()
        {
            this.Client?.Dispose();
        }

        public async Task InitializeAsync()
        {

            host = Program.CreateHostBuilder(null)
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseEnvironment("Testing");
                    webHost.UseTestServer();
                }).Build();

            await host.Services.GetRequiredService<ApplicationContext>().Database.MigrateAsync();
            // Build and start the IHost
            await host.StartAsync();
            this.Client = host.GetTestClient();
            this.ServiceProvider = host.Services;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public void ResetClient()
        {
            this.Client = host.GetTestClient();
        }
    }
}