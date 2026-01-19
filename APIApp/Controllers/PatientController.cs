using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        PatientService service;

        public PatientController(PatientService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(PatientDTO dto)
        {
            try
            {
                return service.Add(dto)
                    ? Ok("Patient Added")
                    : BadRequest("Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("all")]
        public IActionResult All()
        {
            try
            {
                var data = service.GetAll();

                if (data == null)
                    return NotFound("Patient not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult All(int id)
        {
            try
            {
                var data = service.Get(id);
                if (data == null)
                    return NotFound("Patient not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("searcch/{keyword}")]
        public IActionResult Search(string keyword)
        {
            try
            {
                var data = service.Search(keyword);
                if (data == null)
                    return NotFound("Patient not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("appointment/{id}")]
        public IActionResult History(int id)
        {
            try
            {
                var data = service.GetAppointHistory(id);
                if (data == null)
                    return NotFound("Patient not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("upcommingAppointment/{id}")]
        public IActionResult UpComingAppointment(int id)
        {
            try
            {
                var data = service.GetUpcomingAppointments(id);
                if (data == null)
                    return NotFound("Patient not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Deactivate

    }
}
