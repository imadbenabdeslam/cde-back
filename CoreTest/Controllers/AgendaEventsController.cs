using CoreTest.Context;
using CoreTest.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Controllers
{
    [Produces("application/json")]
    [Route("api/AgendaEvents")]
    public class AgendaEventsController : BaseController
    {
        private readonly CDEContext _context;

        public AgendaEventsController(CDEContext context)
            : base(context)
        {
            _context = context;
        }

        // GET: api/AgendaEvents
        [HttpGet, Route("{page}/{countPerPage}")]
        public IActionResult GetAgendaEvents(int page, int countPerPage)
        {
            if (page != 0 && countPerPage != 0)
            {
                return Ok(_context.AgendaEvents.Skip(page * countPerPage).Take(countPerPage));
            }

            return Ok(_context.AgendaEvents);
        }

        [HttpGet, Route("GetLatest")]
        public IActionResult GetLatestEvents()
        {
            var lastT = _context.AgendaEvents.Skip(Math.Max(0, _context.AgendaEvents.Count() - 3)).Take(3);
            return Ok(lastT);
        }

        // GET: api/AgendaEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgendaEvent([FromRoute] int id)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            var agendaEvent = await _context.AgendaEvents.SingleOrDefaultAsync(m => m.Id == id);

            if (agendaEvent == null)
            {
                return NotFound();
            }

            return Ok(agendaEvent);
        }

        // PUT: api/AgendaEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaEvent([FromRoute] int id, [FromBody] AgendaEvent agendaEvent)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            if (id != agendaEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(agendaEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AgendaEvents
        [HttpPost]
        public async Task<IActionResult> PostAgendaEvent([FromBody] AgendaEvent agendaEvent)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            _context.AgendaEvents.Add(agendaEvent);
            await _context.SaveChangesAsync();

            _context.SaveChanges();

            return CreatedAtAction("GetAgendaEvent", new { id = agendaEvent.Id }, agendaEvent);
        }

        // DELETE: api/AgendaEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendaEvent([FromRoute] int id)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            var agendaEvent = await _context.AgendaEvents.SingleOrDefaultAsync(m => m.Id == id);
            if (agendaEvent == null)
            {
                return NotFound();
            }

            _context.AgendaEvents.Remove(agendaEvent);
            await _context.SaveChangesAsync();

            return Ok(agendaEvent);
        }

        private bool AgendaEventExists(int id)
        {
            return _context.AgendaEvents.Any(e => e.Id == id);
        }
    }
}