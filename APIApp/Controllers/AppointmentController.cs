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
    }
}
