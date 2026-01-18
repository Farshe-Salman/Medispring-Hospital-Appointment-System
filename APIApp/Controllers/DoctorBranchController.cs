using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorBranchController : ControllerBase
    {
        DoctorBranchService service;

        public DoctorBranchController(DoctorBranchService service)
        {
            this.service = service;
        }

        [HttpPost("assign")]
        public IActionResult AssignDoctorToBranch(DoctorBranchDTO d)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var res = service.AssignDrToBranch(d);

                if (res.Success)
                {
                    return Ok(res.Message);
                }
                return BadRequest(res.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("branch/{bId})")]
        public IActionResult GetDoctorsByBranch(int bId)
        {
            try
            {
                var data = service.GetDoctorsByBranch(bId);

                if (data == null || data.Count == 0)
                {
                    return NotFound("No doctors Found For this Branch");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("No doctors Found For this Branch");
            }
        }

        [HttpGet("doctor/dId")]
        public IActionResult GetBranchesByDoctor(int dId)
        {
            try
            {
                var data = service.GetBranchesByDoctor(dId);

                if (data == null || data.Count == 0)
                {
                    return NotFound("No Branch Found For this Doctor");
                }
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500) ;
            }
        }
    }
}
