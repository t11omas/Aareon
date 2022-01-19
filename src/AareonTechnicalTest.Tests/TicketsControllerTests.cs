using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AareonTechnicalTest.Contracts;
using AareonTechnicalTest.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AareonTechnicalTest.Tests
{
    public class TicketsControllerTests : IntegrationTestsBase
    {
        public TicketsControllerTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GivenANewTicketRequest_WhenContentIsFilledIn_ThenANewTicketShouldBeCreated()
        {
            // ARRANGE
            var person = await this.UseNonAdminPerson();
            var request = new CreateTicket() { Content = "This is a test ticket" };

            // ACT
            var response = await PostAsync("api/Tickets", request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdTicket = await response.Content.ReadFromJsonAsync<Ticket>();
            createdTicket.Content.Should().Be(request.Content);
            createdTicket.PersonId.Should().Be(person.Id);

            // This would either be pulled into a dedicated audit test in a commercial application
            var auditCount = await Database.AuditEntries.CountAsync(x => x.EntityTypeName == nameof(Ticket));
            auditCount.Should().Be(1);
        }
    }
}