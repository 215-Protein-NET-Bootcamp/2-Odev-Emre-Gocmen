using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odev2.CompanyManagementAPI
{
    [Route("CompanyManagementAPI/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        FolderRepository folderRepository;

        public FolderController(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            Log.Information($"{User.Identity?.Name}: get Folders.");

            var result = new BaseResponse<IEnumerable<Folder>>(await folderRepository.GetAllAsync());

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int folderId)
        {
            Log.Information($"{User.Identity?.Name}: get a Folder with Id is {folderId}.");

            var result = new BaseResponse<Folder>( await folderRepository.GetByIdAsync(folderId) );

            if (!result.Success)
                return BadRequest(result);

            if (result.Response is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Folder resource)
        {
            Log.Information($"{User.Identity?.Name}: create a Folder.");

            await folderRepository.InsertAsync(resource);

            var insertResult = new BaseResponse<Folder>(resource);

            if (!insertResult.Success)
                return BadRequest(insertResult);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Folder resource)
        {
            await folderRepository.UpdateAsync(resource);

            var updateResult = new BaseResponse<Folder>(resource);

            if (updateResult.Success)
                return Ok(updateResult);

            return BadRequest(updateResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int folderId)
        {
            await folderRepository.RemoveAsync(folderId);

            return Ok();
        }
    }
}
