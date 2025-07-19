using AutoMapper;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Classes.Dtos.Responses;

public class ClassMappingProfile  : Profile
{
    public ClassMappingProfile()
    {
        CreateMap<Class, ClassWithTeacherDto>()
            .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher));
        CreateMap<Teacher, TeacherDto>();
        CreateMap<User, UserDto>();

        CreateMap<Class, CreateClassResponseDto>();
    }
}
