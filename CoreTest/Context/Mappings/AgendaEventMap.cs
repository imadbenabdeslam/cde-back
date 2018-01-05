using CoreTest.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreTest.Context.Mappings
{
    public class AgendaEventMap : CoreEntityMap<AgendaEvent>
    {
        public override void Configure(EntityTypeBuilder<AgendaEvent> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Title)
                .HasMaxLength(200);

            builder
                .Property(x => x.Description)
                .HasMaxLength(1000);

            builder
                .Property(x => x.DateEvent)
                .HasMaxLength(50);
        }
    }
}
