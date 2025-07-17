using AutoMapper;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Students.Dtos.Responses;

public class StudentMappingProfile  : Profile
{
    public StudentMappingProfile ()
    {
        CreateMap<Student, StudentWithUserDto>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StudentNumber, opt => opt.MapFrom(src => src.StudentNumber))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt));
    }
}
