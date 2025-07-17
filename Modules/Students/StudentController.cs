namespace SchoolManagementSystem.Controllers;

using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Students.Dtos.Requests;
using SchoolManagementSystem.Students.Dtos.Responses;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Students.Services.Interfaces;
using SchoolManagementSystem.Common.Requests;


    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto dto)
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
                var student = await _studentService.AddStudentAsync(dto);

                return Ok(new ApiResponse<StudentWithUserDto>
                {
                    Success = true,
                    Message = "Teacher created successfully",
                    Data = student
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
        public async Task<IActionResult> GetStudentsPaged([FromQuery] PaginationDto request)
        {
            var (data, pagination) = await _studentService.GetStudentsPagedAsync(request);
                
            var response = new ApiResponse<IEnumerable<StudentWithUserDto>>
                {
                    Success = true,
                    Message = "Data murid berhasil diambil.",
                    Data = data,
                    Meta = new Meta
                    {
                        Pagination = pagination
                    }
                };
                    
                return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            if (id != dto.StudentId)
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
                var updated = await _studentService.UpdateStudentAsync(dto);

                var response = new ApiResponse<StudentWithUserDto>
                {
                    Success = true,
                    Message = "Data murid berhasil diperbarui.",
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
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var success = await _studentService.DeleteStudentAsync(id);
                if (!success)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Murid tidak ditemukan.",
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Murid berhasil dihapus.",
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
