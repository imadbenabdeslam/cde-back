using CoreTest.Context;
using CoreTest.Models.Entities;
using CoreTest.Repositories.Interfaces;

namespace CoreTest.Repositories
{
    public class AgendaEventRepository : BaseRepository<AgendaEvent>, IAgendaEventRepository
    {
        public AgendaEventRepository(CDEContext context)
            : base(context)
        {

        }
    }
}
