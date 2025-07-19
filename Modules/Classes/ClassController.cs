namespace SchoolManagementSystem.Classes.Controllers;

using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Classes.Services.Interfaces;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Classes.Dtos.Requests;
using SchoolManagementSystem.Classes.Dtos.Responses;
using SchoolManagementSystem.Common.Requests;

[ApiController]
[Route("api/classes")]
public class ClassController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpPost]
    public async Task<IActionResult> AddClass([FromBody] CreateClassDto dto)
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
            var newclass = await _classService.AddClassAsync(dto);
            return Ok(new ApiResponse<CreateClassResponseDto>
            {
                Success = true,
                Message = "Class created successfully",
                Data = newclass
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

    [HttpPost("{classId}/assign-teacher/{teacherId}")]
    public async Task<IActionResult> AssignTeacher(int classId, int teacherId)
    {
        try
        {
            var result = await _classService.AssignTeacherAsync(classId, teacherId);
            if (result == null)
            {
                return NotFound(new ApiResponse<ClassWithTeacherDto>
                {
                    Success = false,
                    Message = "kelas atau guru tidak ditemukan",
                });
            }

            return Ok(new ApiResponse<ClassWithTeacherDto>
            {
                Success = true,
                Message = "Berhasil assign kelas ke guru",
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

    [HttpPost("{classId}/unassign-teacher")]
    public async Task<IActionResult> UnssignTeacher(int classId)
    {
        try
        {
            var result = await _classService.UnassignTeacherAsync(classId);
            if (result == null)
            {
                return NotFound(new ApiResponse<ClassWithTeacherDto>
                {
                    Success = false,
                    Message = "kelas tidak ditemukan",
                });
            }

            return Ok(new ApiResponse<ClassWithTeacherDto>
            {
                Success = true,
                Message = "Berhasil unassign kelas",
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
    public async Task<IActionResult> GetClassesPaged([FromQuery] PaginationDto request)
    {
        try
        {
            var (data, pagination) = await _classService.GetClassesPagedAsync(request);

            var response = new ApiResponse<IEnumerable<ClassWithTeacherDto>>
            {
                Success = true,
                Message = "Data kelas berhasil diambil.",
                Data = data,
                Meta = new Meta
                {
                    Pagination = pagination
                }
            };
                
            return Ok(response);
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

}