using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;

namespace CoreTest.Core.GraphQL.Types
{
    public class AgendaEventType : BaseType<AgendaEvent>
    {
        public AgendaEventType(IAgendaEventRepository repository)
            : base(repository)
        {
            Field(x => x.Title).Description("Title of the AgendaEvent");
            Field(x => x.Description).Description("Description fo the AgendaEvent");
            Field(x => x.DateEvent).Description("When the event should take place");

            // In the case of nested entity of another type
            // Here we return the products of the current category
            //Field<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: context => productRepository.GetProductsWithByCategoryIdAsync(context.Source.Id).Result.ToList()
            //);
        }
    }
}
