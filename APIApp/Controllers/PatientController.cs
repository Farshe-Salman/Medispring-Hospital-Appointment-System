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


        [HttpGet("search/{keyword}")]
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

        [HttpPatch("update")]
        public IActionResult Update(PatientDTO p)
        {
            try
            {
                var res = service.Update(p);
                if (res == true)
                    return Ok("Patient Updated");
                return BadRequest("Patinet update failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deactivate/{id}")]
        public IActionResult Deactivate(int id)
        {
            try
            {
                var res = service.Deactivate(id);

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

        [HttpPost("activate/{id}")]
        public IActionResult Activate(int id)
        {
            try
            {
                var res = service.Activate(id);

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

    }
}
