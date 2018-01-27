using CoreTest.Context;
using CoreTest.Core;
using CoreTest.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
            try
            {
                Log.Information("AgendaEventsController.GetAgendaEvents{0}/{1} -- Started".Format(page, countPerPage));

                if (page != 0 && countPerPage != 0)
                {
                    return ProcessResponse(_context.AgendaEvents.Skip(page * countPerPage).Take(countPerPage));
                }

                return ProcessResponse(_context.AgendaEvents);
            }
            catch (Exception ex)
            {

                Log.Error("AgendaEventsController.GetAgendaEvents -- Error occured".Format(page, countPerPage));
                return ProcessResponse(null, ex);
            }
        }

        [HttpGet, Route("GetLatest")]
        public IActionResult GetLatestEvents()
        {
            try
            {
                Log.Information("AgendaEventsController.GetLatestEvents -- Started");

                var lastT = _context.AgendaEvents.Skip(Math.Max(0, _context.AgendaEvents.Count() - 3)).Take(3);
                return ProcessResponse(lastT);
            }
            catch (Exception ex)
            {
                Log.Information("AgendaEventsController.GetLatestEvents -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // GET: api/AgendaEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgendaEvent([FromRoute] int id)
        {
            try
            {
                Log.Information("AgendaEventsController.get by id -- Started");
                var agendaEvent = await _context.AgendaEvents.SingleOrDefaultAsync(m => m.Id == id);

                if (agendaEvent == null)
                {
                    return NotFound();
                }

                return ProcessResponse(agendaEvent);
            }
            catch (Exception ex)
            {
                Log.Information("AgendaEventsController.Get by id -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // PUT: api/AgendaEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaEvent([FromRoute] int id, [FromBody] AgendaEvent agendaEvent)
        {
            try
            {
                Log.Information("AgendaEventsController.Put -- Started");
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

                return ProcessResponse(true);
            }
            catch (Exception ex)
            {
                Log.Information("AgendaEventsController.put -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // POST: api/AgendaEvents
        [HttpPost]
        public async Task<IActionResult> PostAgendaEvent([FromBody] AgendaEvent agendaEvent)
        {
            try
            {
                Log.Information("AgendaEventsController.Post -- Started");
                if (base.IsAuthorized() == false)
                {
                    return Unauthorized();
                }

                _context.AgendaEvents.Add(agendaEvent);
                await _context.SaveChangesAsync();

                _context.SaveChanges();

                return ProcessResponse(agendaEvent.Id);
            }
            catch (Exception ex)
            {
                Log.Information("AgendaEventsController.Post -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // DELETE: api/AgendaEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendaEvent([FromRoute] int id)
        {
            try
            {
                Log.Information("AgendaEventsController.Delete -- Started");
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

                return ProcessResponse(agendaEvent);
            }
            catch (Exception ex)
            {
                Log.Information("AgendaEventsController.Delete -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        private bool AgendaEventExists(int id)
        {
            return _context.AgendaEvents.Any(e => e.Id == id);
        }
    }
}