using AutoMapper;
using Control.Core.DTOs.Countries.Response;
using Control.Core.DTOs.DocumentType.Response;
using Control.Domain.Entities;
using System.Linq;

namespace Control.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            
            CreateMap<DocumentType, DocumentTypeDtoResponse>();
            CreateMap<Countries, CountriesDtoResponse>();
           /*.ForMember(dest => dest.States, opt => opt.MapFrom(src => src.States.Select(s => new States
           {
               IdState = s.IdState,
               NameState = s.NameState,
               Status = s.Status,
               IdCountry = s.IdCountry
           })));*/
            CreateMap<States, StatesDtoResponse>();
        }
    }
}