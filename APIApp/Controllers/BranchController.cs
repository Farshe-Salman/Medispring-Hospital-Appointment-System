using BLL.DTOs;
using BLL.Services;
using DAL.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        BranchService service;

        public BranchController(BranchService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(BranchDTO b)
        {
            try
            {
                var response = service.Add(b);
                if(response==true)
                {
                    return Ok("Account Created Successfully");
                }
                else
                {
                    return BadRequest("Account is not created");
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

        [HttpGet("id/{id}")]
        public IActionResult Get(int id)
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

        [HttpGet("name/{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                var data = service.Get(name);
                if (data == null)
                    return NotFound("Data not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(BranchDTO b)
        {
            try
            {
                var data = service.Update(b);
                return Ok("Branch Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int d = id;
                var data = service.Delete(id);
                if (data == true)
                {
                    return Ok("id " + d + " is removed");
                }
                else
                {
                    return BadRequest("id " + d + " Branch not removed from the system");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
