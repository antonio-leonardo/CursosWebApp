using AutoMapper;
using Cursos.Application.Dto;
using Cursos.Domain.Domain.Models;

namespace Cursos.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CourseDto, Course>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CourseTitle))
                .ForMember(dest => dest.Workload, opt => opt.MapFrom(src => src.Workload))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.Instructor.CompleteName, opt => opt.MapFrom(src => src.InstructorName))
                .ForPath(dest => dest.KnowledgePlatform.Name, opt => opt.MapFrom(src => src.KnowledgePlatform));

            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Workload, opt => opt.MapFrom(src => src.Workload))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.CompleteName))
                .ForPath(dest => dest.KnowledgePlatform, opt => opt.MapFrom(src => src.KnowledgePlatform.Name));
        }
    }
}