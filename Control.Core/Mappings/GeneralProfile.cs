using AutoMapper;
using Control.Core.DTOs.DocumentType.Response;
using Control.Domain.Entities;

namespace Control.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<DocumentType, DocumentTypeDtoResponse>();
        }
    }
}