using CoreTest.Context;
using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;

namespace CoreTest.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(CDEContext context)
            : base(context)
        {

        }
    }
}
