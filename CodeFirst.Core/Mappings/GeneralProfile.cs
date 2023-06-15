using AutoMapper;
using CodeFirst.Core.DTOs.DocumentType.Response;
using CodeFirst.Domain.Entities;

namespace CodeFirst.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<DocumentType, DocumentTypeDtoResponse>();
        }
    }
}