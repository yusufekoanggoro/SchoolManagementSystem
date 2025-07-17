using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Teachers.Dtos.Requests;
using SchoolManagementSystem.Teachers.Dtos.Responses;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Teachers.Services.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDto dto)
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
                var teacher = await _teacherService.AddTeacherAsync(dto);
                return Ok(new ApiResponse<TeacherWithUserDto>
                {
                    Success = true,
                    Message = "Teacher created successfully",
                    Data = teacher
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
        public async Task<IActionResult> GetTeachersPaged([FromQuery] PaginationDto request)
        {
            var (data, pagination) = await _teacherService.GetTeachersPagedAsync(request);

            var response = new ApiResponse<IEnumerable<TeacherWithUserDto>>
            {
                Success = true,
                Message = "Data guru berhasil diambil.",
                Data = data,
                Meta = new Meta
                {
                    Pagination = pagination
                }
            };
                
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] UpdateTeacherDto dto)
        {
            if (id != dto.TeacherId)
            {
                return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "ID pada URL dan body tidak sesuai.",
                    });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validasi gagal.",
                    Data = ModelState.ToString()
                });
            }

            try
            {
                var updated = await _teacherService.UpdateTeacherAsync(dto);

                var response = new ApiResponse<TeacherWithUserDto>
                {
                    Success = true,
                    Message = "Data guru berhasil diperbarui.",
                    Data = updated
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
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var success = await _teacherService.DeleteTeacherAsync(id);
                if (!success)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Guru tidak ditemukan.",
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Guru berhasil dihapus.",
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

    }
}