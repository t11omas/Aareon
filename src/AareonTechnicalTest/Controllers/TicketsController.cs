using System.Linq;
using System.Threading.Tasks;
using AareonTechnicalTest.Contracts;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationContext dbContext;

        public TicketsController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? personId)
        {
            var query = dbContext.Tickets.AsQueryable();

            if (personId.HasValue)
            {
                query = dbContext.Tickets.Where(x => x.PersonId == personId);
            }

            var tickets = await query.AsNoTracking().ToListAsync(Request.HttpContext.RequestAborted);
            return this.Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ticket = await dbContext.Tickets.FindAsync(id, HttpContext.RequestAborted);
            return this.Ok(ticket);
        }

       [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTicket request)
        {
            var ticket = new Ticket()
            {
                PersonId = User.GetUserId(),
                Content = request.Content,
            };

            await dbContext.AddAsync(ticket);
            await dbContext.SaveChangesAsync(HttpContext.RequestAborted);

            return this.CreatedAtAction(nameof(this.Get), new { id = ticket.Id }, ticket); ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTicket request)
        {
            var ticket = await dbContext.Tickets.FindAsync(id, HttpContext.RequestAborted);

            if (ticket is null)
            {
                return this.NotFound();
            }

            ticket.Content = request.Content;
            await dbContext.SaveChangesAsync(HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await dbContext.Tickets.FindAsync(id, HttpContext.RequestAborted);

            if (ticket is not null)
            {
                dbContext.Remove(ticket);
                await dbContext.SaveChangesAsync(HttpContext.RequestAborted);
            }

            return NoContent();
        }
    }
}