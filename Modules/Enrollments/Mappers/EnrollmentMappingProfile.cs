using AutoMapper;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Enrollments.Dtos.Responses;

public class EnrollmentMappingProfile  : Profile
{
    public EnrollmentMappingProfile()
    {
        CreateMap<Enrollment, CreateEnrollmentResponseDto>();

        CreateMap<Enrollment, EnrollmentWithStudentAndClassDto>()
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.User.FullName))
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
    }
}
