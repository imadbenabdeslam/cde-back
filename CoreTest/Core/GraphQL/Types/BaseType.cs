using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;
using GraphQL.Types;

namespace CoreTest.Core.GraphQL.Types
{
    public class BaseType<TEntity> : ObjectGraphType<TEntity>
        where TEntity : Entity
    {
        public BaseType(IBaseRepository<TEntity> repository)
        {
            Field(x => x.Id).Description("Id of the Entity");
        }
    }
}
