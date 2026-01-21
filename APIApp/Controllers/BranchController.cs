using BLL.DTOs;
using BLL.Services;
using DAL.EF.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin,branchManager")]
        [HttpPost("add")]
        public IActionResult Add(AddBranchDTO b)
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


        [HttpGet("{id}")]
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

        [HttpGet("search/{keyword}")]
        public IActionResult Search(string keyword)
        {
            try
            {
                var data = service.Search(keyword);
                if (data == null)
                    return NotFound("Data not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,branchManager")]
        [HttpPatch("update")]
        public IActionResult Update(BranchDTO b)
        {
            try
            {
                var res = service.Update(b);
                if (res == true)
                    return Ok("Branch Updated");
                return BadRequest("Branch update failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,branchManager")]
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

        [Authorize(Roles = "admin,branchManager")]
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
    }
}
