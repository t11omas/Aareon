using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AareonTechnicalTest.Tests
{
    public class NotesControllerTests : IntegrationTestsBase
    {
        public NotesControllerTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GivenANoteExists_WhenAnAdminRequestsToDeleteIt_ThenTheNoteShouldBeDeleted()
        {
            // ARRANGE
            var person = await this.UseAdminPerson();
            var ticket = new Ticket()
            {
                PersonId = person.Id, 
                Content = "This is a test ticket",
                Notes = new List<Note>() { new () { PersonId = person.Id, Content = "This is a note" } }
            };
            await Database.AddAsync(ticket);
            await Database.SaveChangesAsync(CancellationToken.None);

            // ACT
            var response = await DeleteAsync($"api/Tickets/{ticket.Id}/Notes/{ticket.Notes.First().Id}");

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // This would either be pulled into a dedicated audit test in a commercial application
            var auditCount = await Database.AuditEntries.CountAsync(x => x.EntityTypeName == nameof(Note) && x.StateName == "EntityDeleted");
            auditCount.Should().Be(1);
        }

        [Fact]
        public async Task GivenANoteExists_WhenANonAdminRequestsToDeleteIt_ThenTheNoteShouldNotBeDeleted()
        {
            // ARRANGE
            var person = await this.UseNonAdminPerson();
            var ticket = new Ticket()
            {
                PersonId = person.Id,
                Content = "This is a test ticket",
                Notes = new List<Note>() { new() { PersonId = person.Id, Content = "This is a note" } }
            };
            await Database.AddAsync(ticket);
            await Database.SaveChangesAsync(CancellationToken.None);

            // ACT
            var response = await DeleteAsync($"api/Tickets/{ticket.Id}/Notes/{ticket.Notes.First().Id}");

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            // This would either be pulled into a dedicated audit test in a commercial application
            var auditCount = await Database.AuditEntries.CountAsync(x => x.EntityTypeName == nameof(Note) && x.StateName == "EntityDeleted");
            auditCount.Should().Be(0);
        }
    }
}