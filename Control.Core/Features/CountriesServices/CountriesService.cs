using AutoMapper;
using Control.Core.DTOs.Countries.Request;
using Control.Core.DTOs.Countries.Response;
using Control.Core.DTOs.DocumentType.Response;
using Control.Core.Interfaces.Repositories;
using Control.Core.Interfaces.Services;
using Control.Domain.Entities;
using Control.Domain.Exceptions;
using Control.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Core.Features.CountriesServices
{
    public class CountriesService:ICountriesService
    {
        readonly IUnitOfWork _UnitOfWork;
        readonly IMapper _Mapper; 
        public CountriesService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = Mapper;
        }

        //autor: angel.lizcano
        //GetCountry: smsmsmsmsms ms asm damns dmnas dmnas d
        public async Task<Response<IEnumerable<CountriesDtoResponse>>> GetCountry()
        {
            IEnumerable<Countries> searchCountries = await _UnitOfWork.CountriesRepositoryAsync.GetAsync(includeProperties:"States");
            if (searchCountries == null) { throw new CoreException("La información solicitada no es exitosa."); }

            IEnumerable<CountriesDtoResponse> countriesDtoList = _Mapper.Map<IEnumerable<CountriesDtoResponse>>(searchCountries);

            return new Response<IEnumerable<CountriesDtoResponse>>(countriesDtoList) { Message = "La información solicitada ha sido exitosa." };
        }


    }
}
