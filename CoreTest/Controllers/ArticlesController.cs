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
    [Route("api/Articles")]
    public class ArticlesController : BaseController
    {
        private readonly CDEContext _context;

        public ArticlesController(CDEContext context)
            : base(context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet, Route("{page}/{countPerPage}")]
        public IActionResult GetArticles(int page, int countPerPage)
        {
            if (page != 0 && countPerPage != 0)
            {
                return Ok(_context.Articles.Skip(page * countPerPage).Take(countPerPage));
            }

            return Ok(_context.Articles);
        }

        // GET: api/Articles
        [HttpGet, Route("GetLatest")]
        public IActionResult GetLatestArticles()
        {
            var lastT = _context.Articles.Skip(Math.Max(0, _context.Articles.Count() - 5)).Take(5);
            return Ok(lastT);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle([FromRoute] int id, [FromBody] Article article)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            if (base.IsAuthorized() == false)
            {
                return Unauthorized();
            }

            // ToDo(ibe): May establish default value if value received null

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Ok(article);
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}