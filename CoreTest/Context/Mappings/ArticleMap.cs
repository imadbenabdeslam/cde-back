using CoreTest.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreTest.Context.Mappings
{
    public class ArticleMap : CoreEntityMap<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Tag)
                .HasMaxLength(150);

            builder
                .Property(x => x.Title)
                .HasMaxLength(250);

            builder
                .Property(x => x.Subtitle)
                .HasMaxLength(250);
        }
    }
}