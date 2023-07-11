using Control.Core.DTOs.Countries.Request;
using Control.Core.DTOs.Countries.Response;
using Control.Domain.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Control.Core.Interfaces.Services
{
    public interface ICountriesService
    {
        Task<Response<IEnumerable<CountriesDtoResponse>>> GetCountry();
    }
}
