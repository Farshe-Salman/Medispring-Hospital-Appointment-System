using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorScheduleController : ControllerBase
    {
        DoctorScheduleService service;

        public DoctorScheduleController(DoctorScheduleService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(DoctorScheduleDTO dto)
        {
            try
            {
                var result = service.Add(dto);

                if (result.Success)
                    return Ok(result.Message);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public IActionResult GetByDoctor(int doctorId)
        {
            try
            {
                return Ok(service.GetByDoctor(doctorId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("branch/{branchId}")]
        public IActionResult GetByBranch(int branchId)
        {
            try
            {
                return Ok(service.GetByBranch(branchId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(DoctorScheduleUpdateDto dto)
        {
            try
            {
                var res = service.Update(dto);

                if(res.Success)
                {
                    return Ok(res.Message);
                }
                return BadRequest(res.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
