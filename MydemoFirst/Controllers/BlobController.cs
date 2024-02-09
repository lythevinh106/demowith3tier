using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MydemoFirst.Services.Contracts;

using MydemoFirst.Services.StorageServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlob _blob;

        public BlobController(IBlob blob)
        {
            _blob = blob;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BlobUpdate([FromForm] BlobRequest blobRequest)
        {
            await using (Stream fileStream = blobRequest.file.OpenReadStream())
            {
                string contentType = blobRequest.file.ContentType;
                var result = await _blob.UploadBlobFile(fileStream, blobRequest.file.FileName, contentType);
                return Ok(result);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> BlobDelete([FromBody] BlobDelete blobDelete)
        {
            bool result = await _blob.DeleteBlobAsync(blobDelete.BlobFileName);

            return Ok(result);
        }

        [HttpGet("download")]
        public async Task<IActionResult> BlobDownload([FromBody] BlobDownload blobDownload)
        {
            return Ok(await _blob.DownLoadBlobAsync(blobDownload.BlobFileName));
        }

        [HttpPost("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}