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
            try
            {
                Log.Information("ArticlesController.GetArticles/{0}/{1} -- Started".Format(page, countPerPage));
                if (page != 0 && countPerPage != 0)
                {
                    return ProcessResponse(_context.Articles.Skip(page * countPerPage).Take(countPerPage));
                }

                return ProcessResponse(_context.Articles);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.GetArticles -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // GET: api/Articles
        [HttpGet, Route("GetLatest")]
        public IActionResult GetLatestArticles()
        {
            try
            {
                Log.Information("ArticlesController.GetLatest -- Started");
                var lastT = _context.Articles.Skip(Math.Max(0, _context.Articles.Count() - 5)).Take(5);
                return ProcessResponse(lastT);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.GetLatest -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] int id)
        {
            try
            {
                Log.Information("ArticlesController.Get by id -- Started");
                var article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);

                if (article == null)
                {
                    return NotFound();
                }

                return ProcessResponse(article);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.Get by id -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle([FromRoute] int id, [FromBody] Article article)
        {
            try
            {
                Log.Information("ArticlesController.Put -- Started");
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

                return ProcessResponse(true);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.Put -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            try
            {
                Log.Information("ArticlesController.Post -- Started");
                if (base.IsAuthorized() == false)
                {
                    return Unauthorized();
                }

                // ToDo(ibe): May establish default value if value received null

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                return ProcessResponse(article.Id);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.Post -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            try
            {
                Log.Information("ArticlesController.Delete -- Started");
                if (base.IsAuthorized() == false)
                {
                    return Unauthorized();
                }

                var article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);
                if (article == null)
                {
                    return NotFound();
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();

                return ProcessResponse(article);
            }
            catch (Exception ex)
            {
                Log.Error("ArticlesController.Delete -- An error occured");
                return ProcessResponse(null, ex);
            }
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}