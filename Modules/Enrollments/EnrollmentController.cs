namespace SchoolManagementSystem.Enrollments.Controllers;

using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Enrollments.Services.Interfaces;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Enrollments.Dtos.Requests;
using SchoolManagementSystem.Enrollments.Dtos.Responses;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentController : ControllerBase
{

    private readonly IEnrollmentService _enrollmentService;
    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> EnrollStudentAsync([FromBody] CreateEnrollmentDto dto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Validation error",
                Data = ModelState.ToString(),
            });
        }

        try
        {
            var result = await _enrollmentService.EnrollStudentAsync(dto);
            return Ok(new ApiResponse<CreateEnrollmentResponseDto>
            {
                Success = true,
                Message = "Berhasil enroll",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Internal server error",
                Data = ex.Message
            });
        }

    }
    
    [HttpGet]
    public async Task<IActionResult> GetEnrollmentsPaged([FromQuery] PaginationDto request)
        {
            var (data, pagination) = await _enrollmentService.GetEnrollmentsPagedAsync(request);

            var response = new ApiResponse<IEnumerable<EnrollmentWithStudentAndClassDto>>
            {
                Success = true,
                Message = "Data enroll berhasil diambil.",
                Data = data,
                Meta = new Meta
                {
                    Pagination = pagination
                }
            };
                
            return Ok(response);
        }

}