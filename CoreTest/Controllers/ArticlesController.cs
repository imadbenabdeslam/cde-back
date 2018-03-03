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