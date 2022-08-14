using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    [Route("CompanyManagementAPI/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        CountryRepository countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            Log.Information($"{User.Identity?.Name}: get Countries.");

            var result = new BaseResponse<IEnumerable<Country>>(await countryRepository.GetAllAsync());

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int countryId)
        {
            Log.Information($"{User.Identity?.Name}: get a Country with Id is {countryId}.");

            var result = new BaseResponse<Country>( await countryRepository.GetByIdAsync(countryId) );

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country resource)
        {
            Log.Information($"{User.Identity?.Name}: create a Country.");

            await countryRepository.InsertAsync(resource);

            var insertResult = new BaseResponse<Country>(resource);

            if (!insertResult.Success)
                return BadRequest(insertResult);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Country resource)
        {
            await countryRepository.UpdateAsync(resource);

            var updateResult = new BaseResponse<Country>(resource);

            if (updateResult.Success)
                return Ok(updateResult);

            return BadRequest(updateResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int countryId)
        {
            await countryRepository.RemoveAsync(countryId);

            return Ok();
        }
    }
}
