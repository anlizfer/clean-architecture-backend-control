using Control.Domain.Entities;
using Control.Infrastructure.Settings;

namespace Control.Infrastructure.Repositories.RespositoryAsync
{
    public class CountriesRepository : GenericRepository<Countries>
    {
        public CountriesRepository(ControlContext ControlContext) : base(ControlContext)
        {

        }
    }
}
