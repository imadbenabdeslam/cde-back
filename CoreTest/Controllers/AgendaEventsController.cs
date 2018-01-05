using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreTest.Context;
using CoreTest.Models.Entities;

namespace CoreTest.Controllers
{
    [Produces("application/json")]
    [Route("api/AgendaEvents")]
    public class AgendaEventsController : Controller
    {
        private readonly CDEContext _context;

        public AgendaEventsController(CDEContext context)
        {
            _context = context;
        }

        // GET: api/AgendaEvents
        [HttpGet]
        public IEnumerable<AgendaEvent> GetAgendaEvents()
        {
            return _context.AgendaEvents;
        }

        // GET: api/AgendaEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgendaEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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