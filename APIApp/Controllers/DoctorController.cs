using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorService service;

        public DoctorController(DoctorService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(DoctorDTO d)
        {
            try
            {
                var response = service.Add(d);
                if (response == true)
                {
                    return Ok("Doctor Added Successfully");
                }
                else
                {
                    return BadRequest("Doctor is not addedd");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var data = service.GetAll();
                if (data == null)
                    return NotFound("Data not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("find/id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = service.Get(id);
                if (data == null)
                    return NotFound("Data not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("find/name/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var data = service.SearchByName(name);
                if (data.Count == 0)
                    return NotFound("Data not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("update")]
        public IActionResult Update(DoctorDTO d)
        {
            try
            {
                var res = service.Update(d);
                if (res == true)
                    return Ok("Doctor Updated");
                return BadRequest("Doctor update failed");
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
                int d = id;
                var data = service.Deactivate(id);
                if (data == true)
                {
                    return Ok("id " + d + " is now deactivate");
                }
                else
                {
                    return BadRequest("id " + d + " Doctor not deactivated from the system");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("active/doctor")]
        public IActionResult GetActiveDoctors()
        {
            try
            {
                var data = service.GetActiveDoctors();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
