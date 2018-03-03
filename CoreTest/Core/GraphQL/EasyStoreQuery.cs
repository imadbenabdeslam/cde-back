using CoreTest.Core.GraphQL.Types;
using CoreTest.Repositories.Interfaces;
using GraphQL.Types;
using System;
using System.Linq;

namespace CoreTest.Core.GraphQL
{
    public class EasyStoreQuery : ObjectGraphType
    {
        /// <summary>
        /// Queries
        /// </summary>
        /// <param name="agendaEventRepository">Agenda Event Repository</param>
        public EasyStoreQuery(IAgendaEventRepository agendaEventRepository, IArticleRepository articleRepository)
        {
            AgendaEventQuery(agendaEventRepository);
            ArticleQuery(articleRepository);
        }

        /// <summary>
        /// Article Query
        /// </summary>
        /// <param name="articleRepository"></param>
        private void ArticleQuery(IArticleRepository articleRepository)
        {
            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Id of the article" }
                ),
                resolve: context => articleRepository.GetById(context.GetArgument<int>("id")).Result
            );

            Field<ListGraphType<ArticleType>>(
                "latestArticles",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "take", Description = "The total of entity to return" }
                ),
                resolve: context =>
                {
                    var nbrTake = (int?)context.Arguments["take"];

                    if (nbrTake.HasValue == false)
                    {
                        nbrTake = 5;
                    }

                    var query = articleRepository.GetQueryAsNoTracking().Result.OrderByDescending(x => x.Id);

                    var res = query.Take(nbrTake.Value).ToList();

                    return res;
                }
            );

            Field<ListGraphType<ArticleType>>(
                "articles",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "pageCount", Description = "The total of entity to return" },
                    new QueryArgument<IntGraphType> { Name = "pageNbr", Description = "The page number we are currently on" },
                    new QueryArgument<BooleanGraphType> { Name = "desc", Description = "Specify if we want the result to be ordered backwards" }
                ),
                resolve: context =>
                {
                    var pageCount = (int?)context.Arguments["pageCount"];
                    var pageNbr = (int?)context.Arguments["pageNbr"];
                    var desc = (bool?)context.Arguments["desc"];

                    var result = articleRepository.GetAll().Result;

                    if (desc.HasValue && desc.Value == true)
                    {
                        result = result.Reverse();
                    }

                    if (pageCount.HasValue && pageNbr.HasValue)
                    {
                        result = result.Skip(pageNbr.Value * pageCount.Value).Take(pageCount.Value);
                    }

                    return result;
                }
            );
        }

        /// <summary>
        /// Agenda Event Query
        /// </summary>
        /// <param name="aeRepository">Repository Agenda Event</param>
        public void AgendaEventQuery(IAgendaEventRepository aeRepository)
        {
            Field<AgendaEventType>(
                "agendaevent",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of the Agenda Event" }
                ),
                resolve: context => aeRepository.GetById(context.GetArgument<int>("id")).Result
            );

            Field<ListGraphType<AgendaEventType>>(
                "latestAgendaevents",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "take", Description = "The total of entity to return" }
                ),
                resolve: context =>
                {
                    var nbrTake = (int?)context.Arguments["take"];

                    if (nbrTake.HasValue == false)
                    {
                        nbrTake = 5;
                    }

                    var query = aeRepository.GetQueryAsNoTracking().Result.OrderByDescending(x => x.Id);

                    var res = query.Take(nbrTake.Value).ToList();

                    return res;
                }
            );

            Field<ListGraphType<AgendaEventType>>(
                "agendaevents",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "pageCount", Description = "The total of entity to return" },
                    new QueryArgument<IntGraphType> { Name = "pageNbr", Description = "The page number we are currently on" },
                    new QueryArgument<BooleanGraphType> { Name = "desc", Description = "Specify if we want the result to be ordered backwards" }
                ),
                resolve: context =>
                {
                    var pageCount = (int?)context.Arguments["pageCount"];
                    var pageNbr = (int?)context.Arguments["pageNbr"];
                    var desc = (bool?)context.Arguments["desc"];

                    var result = aeRepository.GetAll().Result;

                    if (desc.HasValue && desc.Value == true)
                    {
                        result = result.Reverse();
                    }

                    if (pageCount.HasValue && pageNbr.HasValue)
                    {
                        result = result.Skip(pageNbr.Value * pageCount.Value).Take(pageCount.Value);
                    }

                    return result;
                }
            );
        }
    }
}
