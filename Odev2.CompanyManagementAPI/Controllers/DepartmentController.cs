using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    [Route("CompanyManagementAPI/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        DepartmentRepository departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            Log.Information($"{User.Identity?.Name}: get Employees.");

            var result = new BaseResponse<IEnumerable<Department>>(await departmentRepository.GetAllAsync());

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int deparmentId)
        {
            Log.Information($"{User.Identity?.Name}: get a Department with Id is {deparmentId}.");

            var result = new BaseResponse<Department>( await departmentRepository.GetByIdAsync(deparmentId) );

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Department resource)
        {
            Log.Information($"{User.Identity?.Name}: create a Department.");

            await departmentRepository.InsertAsync(resource);

            var insertResult = new BaseResponse<Department>(resource);

            if (!insertResult.Success)
                return BadRequest(insertResult);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Department resource)
        {
            await departmentRepository.UpdateAsync(resource);

            var updateResult = new BaseResponse<Department>(resource);

            if (updateResult.Success)
                return Ok(updateResult);

            return BadRequest(updateResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int deparmentId)
        {
            await departmentRepository.RemoveAsync(deparmentId);

            return Ok();
        }
    }
}
