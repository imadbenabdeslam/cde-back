using CoreTest.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreTest.Context.Mappings
{
    public class AdminDataMap : CoreEntityMap<AdminData>
    {
        public override void Configure(EntityTypeBuilder<AdminData> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Password)
                .HasMaxLength(25);

            builder
                .Property(x => x.Token)
                .HasMaxLength(40);
        }
    }
}