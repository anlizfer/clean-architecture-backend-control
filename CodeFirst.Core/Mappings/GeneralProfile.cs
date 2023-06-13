using AutoMapper;
using CodeFirst.Core.DTOs.Account.Responses;
using CodeFirst.Core.DTOs.Course.Response;
using CodeFirst.Core.DTOs.DocumentType.Response;
using CodeFirst.Core.DTOs.Request.Course.Requests;
using CodeFirst.Core.DTOs.Request.Inscription.Requests;
using CodeFirst.Core.DTOs.Security;
using CodeFirst.Core.DTOs.Student.Request;
using CodeFirst.Core.DTOs.Student.Response;
using CodeFirst.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CodeFirst.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            CreateMap<DocumentType,DocumentTypeDtoResponse>();

            CreateMap<StudentAddDtoRequest, Student>();
            CreateMap<Student, StudentDtoResponse>();

            CreateMap<CourseAddDtoRequest, Course>();
            CreateMap<Course, CourseDtoResponse>();

            CreateMap<InscriptionAddDtoRequest, Inscription>();
            CreateMap<IdentityUser, UsuarioDtoResponse>()
                .ForMember(dest =>
                           dest.Id,
                            opt => opt.MapFrom(src => src.Id)
                           )
                .ForMember(dest =>
                           dest.Email,
                           opt => opt.MapFrom(src => src.Email)
                           )
                ;

            CreateMap<SecurityAddDtoRequest, Security>();
            CreateMap<LoginDtoRequest, Login>();
        }
    }
}