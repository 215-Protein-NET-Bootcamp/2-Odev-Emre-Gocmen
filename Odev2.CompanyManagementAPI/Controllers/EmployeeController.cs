using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    [Route("CompanyManagementAPI/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            Log.Information($"{User.Identity?.Name}: get Employees.");

            var result = new BaseResponse<IEnumerable<Employee>>(await employeeRepository.GetAllAsync());

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int employeeId)
        {
            Log.Information($"{User.Identity?.Name}: get an Employee with Id is {employeeId}.");

            var result = new BaseResponse<Employee>( await employeeRepository.GetByIdAsync(employeeId) );

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetByIdWithDetails")]
        public async Task<IActionResult> GetByIdWithDetails(int employeeId)
        {
            Log.Information($"{User.Identity?.Name}: get an Employee with Id is {employeeId}.");

            var result = new BaseResponse<EmployeeDto>(await employeeRepository.GetByIdWithDetailsAsync(employeeId));

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee resource)
        {
            Log.Information($"{User.Identity?.Name}: create an Employee.");

            await employeeRepository.InsertAsync(resource);

            var insertResult = new BaseResponse<Employee>(resource);

            if (!insertResult.Success)
                return BadRequest(insertResult);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee resource)
        {
            await employeeRepository.UpdateAsync(resource);

            var updateResult = new BaseResponse<Employee>(resource);

            if (updateResult.Success)
                return Ok(updateResult);

            return BadRequest(updateResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int employeeId)
        {
            await employeeRepository.RemoveAsync(employeeId);

            return Ok();
        }
    }
}
