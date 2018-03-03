using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;
using GraphQL.Types;

namespace CoreTest.Core.GraphQL.Types
{
    public class ArticleType : BaseType<Article>
    {
        public ArticleType(IArticleRepository repository)
            : base(repository)
        {
            Field(x => x.Title).Description("Title of the AgendaEvent");
            Field(x => x.Tag).Description("The tag of the Article");
            Field(x => x.Content).Description("The content of the article");
            Field(x => x.Subtitle).Description("the Subtitle");
            Field(x => x.Author).Description("The author of the article");
            Field(x => x.DateAvailable).Description("Date when the article should be available");

            Field<IntGraphType>(
                "articleType",
                resolve: context => (int)context.Source.ArticleType
            );

            // In the case of nested entity of another type
            // Here we return the products of the current category
            //Field<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: context => productRepository.GetProductsWithByCategoryIdAsync(context.Source.Id).Result.ToList()
            //);
        }
    }
}
