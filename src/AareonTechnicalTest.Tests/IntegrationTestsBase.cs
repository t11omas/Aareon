using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Z.EntityFramework.Plus;

namespace AareonTechnicalTest.Tests
{
    [Collection("TestServer collection")]
    public abstract class IntegrationTestsBase : IAsyncLifetime
    {
        public readonly TestServerFixture fixture;
        private static string validToken;
        protected ApplicationContext Database { get; private set; }

        protected IntegrationTestsBase(TestServerFixture fixture)
        {
            this.fixture = fixture;
            this.Database = new ApplicationContext(this.fixture.ServiceProvider.GetService<DbContextOptions<ApplicationContext>>());
        }

        public virtual async Task InitializeAsync()
        {
            await this.Database.Tickets.DeleteAsync();
            await this.Database.Persons.DeleteAsync();
            await this.Database.AuditEntries.DeleteAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        protected Task<HttpResponseMessage> GetAsync(string requestUri, string bearer = null)
        {
            if (string.IsNullOrEmpty(bearer))
            {
                bearer = validToken;
            }

            fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
            return fixture.Client.GetAsync(requestUri);
        }

        protected async Task<T> GetAsync<T>(string requestUri, string bearer = null)
        {
            if (string.IsNullOrEmpty(bearer))
            {
                bearer = validToken;
            }

            fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
            var httpResponse = await fixture.Client.GetAsync(requestUri);
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }

        protected Task<HttpResponseMessage> PostAsync<T>(string requestUri, T data, string bearer = null)
        {
            var content = data is null ? null : new StringContent(JsonSerializer.Serialize(data), Encoding.Default, "application/json");
            if (string.IsNullOrEmpty(bearer))
            {
                bearer = validToken;
            }

            fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
            return fixture.Client.PostAsync(requestUri, content);
        }

        protected Task<HttpResponseMessage> PutAsync<T>(string requestUri, T data, string bearer = null)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.Default, "application/json");
            if (string.IsNullOrEmpty(bearer))
            {
                bearer = validToken;
            }

            fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
            return fixture.Client.PutAsync(requestUri, content);
        }

        protected Task<HttpResponseMessage> DeleteAsync(string requestUri, string bearer = null)
        {
            if (string.IsNullOrEmpty(bearer))
            {
                bearer = validToken;
            }

            fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearer);
            return fixture.Client.DeleteAsync(requestUri);
        }

        protected async Task<Person> UseNonAdminPerson()
        {
            var person = new Person()
            {
                Forename = "Test",
                Surname = "User",
                IsAdmin = false,
            };
            await Database.Persons.AddAsync(person);
            await Database.SaveChangesAsync(CancellationToken.None);
            var httpResponse = await fixture.Client.GetAsync($"api/Auth?personId={person.Id}");
            validToken = await httpResponse.Content.ReadAsStringAsync();
            return person;
        }

        protected async Task<Person> UseAdminPerson()
        {
            var person = new Person()
            {
                Forename = "Test",
                Surname = "User",
                IsAdmin = true,
            };
            await Database.Persons.AddAsync(person);
            await Database.SaveChangesAsync(CancellationToken.None);
            var httpResponse = await fixture.Client.GetAsync($"api/Auth?personId={person.Id}");
            validToken = await httpResponse.Content.ReadAsStringAsync();
            return person;
        }
    }
}