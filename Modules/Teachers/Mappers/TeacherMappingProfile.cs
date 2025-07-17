using AutoMapper;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Teachers.Dtos.Responses;

public class TeacherMappingProfile  : Profile
{
    public TeacherMappingProfile ()
    {
        CreateMap<Teacher, TeacherWithUserDto>()
            .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TeacherNumber, opt => opt.MapFrom(src => src.TeacherNumber))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt));
    }
}
