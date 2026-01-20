using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        AppointmentService service;

        public AppointmentController(AppointmentService service)
        {
            this.service = service;
        }

        [HttpPost("book")]
        public IActionResult Book(AppointmentDTO dto)
        {
            try
            {
                var res = service.Book(dto);

                if (res.Success)
                {
                    return Ok(res.Message);
                }
                else
                    return BadRequest(res.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            try
            {
                var res = service.AppointmentList();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost("cancel/{id}")]
        public IActionResult Cancel(int id, [FromBody]  string reason)
        {
            try
            {
                var res = service.Cancel(id, reason);
                if(res.Success)
                {
                    return Ok(res.Message);
                }
                else
                {
                    return BadRequest(res.Message);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("getDailyReport/{date}")]
        public IActionResult GetDailyReport(DateTime  date)
        {
            try
            {
                return Ok(service.GetDailyReport(date));
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
