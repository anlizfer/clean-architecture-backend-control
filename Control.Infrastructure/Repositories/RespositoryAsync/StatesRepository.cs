using Control.Domain.Entities;
using Control.Infrastructure.Settings;

namespace Control.Infrastructure.Repositories.RespositoryAsync
{
    public class StatesRepository : GenericRepository<States>
    {
        public StatesRepository(ControlContext ControlContext) : base(ControlContext)
        {

        }
    }
}
