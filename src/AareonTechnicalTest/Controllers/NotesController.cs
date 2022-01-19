using System.Linq;
using System.Threading.Tasks;
using AareonTechnicalTest.Contracts;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Controllers
{
    [Route("api/tickets/{ticketId}/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationContext dbContext;

        public NotesController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int ticketId)
        {
            var notes = await dbContext.Notes.Where(x => x.TicketId == ticketId).ToListAsync(HttpContext.RequestAborted);
            return this.Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ticketId, int id)
        {
            var note = await dbContext.Notes.Where(x => x.Id == id && x.TicketId == ticketId).ToListAsync(HttpContext.RequestAborted);

            if (note is null)
            {
                return this.NotFound();
            }

            return this.Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int ticketId, [FromBody] CreateNote request)
        {
            var ticket = new Note()
            {
                PersonId = User.GetUserId(),
                Content = request.Content,
                TicketId = ticketId,
            };

            await dbContext.AddAsync(ticket);
            await dbContext.SaveChangesAsync(HttpContext.RequestAborted);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int ticketId,[FromBody] UpdateNote request)
        {
            var note = await dbContext.Notes.Where(x => x.Id == id && x.TicketId == ticketId).FirstOrDefaultAsync(HttpContext.RequestAborted);

            if (note is null)
            {
                return this.NotFound();
            }

            note.Content = request.Content;
            await dbContext.SaveChangesAsync(HttpContext.RequestAborted);

            return this.Ok(note);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ticketId, int id)
        {
            var ticket = await dbContext.Notes.Where(x => x.TicketId == ticketId && x.Id ==id).FirstOrDefaultAsync(HttpContext.RequestAborted);

            if (ticket is not null)
            {
                dbContext.Remove(ticket);
                await dbContext.SaveChangesAsync(HttpContext.RequestAborted);
            }

            return NoContent();
        }
    }
}