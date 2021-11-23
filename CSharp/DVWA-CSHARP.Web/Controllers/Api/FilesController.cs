using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OWASP10_2021.Models;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace OWASP10_2021.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public FilesController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<DirAndFile>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFiles(string path)
        {
            path = Path.Combine(Startup.WebRootPath, path ?? string.Empty);

            if (!Directory.Exists(path))
            {
                return NotFound();
            }

            var Dirs = Directory.GetDirectories(path)
                .Select(x => new DirAndFile() { Name = x.Split('\\').Last(), Path = x, Type = "D" });

            var Files = Directory.GetFiles(path)
                .Select(x => new DirAndFile() { Name = x.Split('\\').Last(), Path = x, Type = "F" });

            return Ok(Dirs.Union(Files));
        }

        [HttpGet("Contents")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetContents(string path, string contentType, bool forcedDownload = false)
        {
            path = Path.Combine(Startup.WebRootPath, path ?? string.Empty);
            
            if (!System.IO.File.Exists(path))
                return NotFound();

            var attr = System.IO.File.GetAttributes(path);
            var fileName = Path.GetFileName(path);

            if (forcedDownload && !attr.HasFlag(FileAttributes.Directory))
            {
                Response.Headers.ContentDisposition = $"attachment;filename=\"{fileName}\"";
            }
            return File(System.IO.File.ReadAllBytes(path), contentType ??= "application/json");
        }

        [HttpPost("Contents")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostContents(string path, byte[] fileContents)
        {
            path = Path.Combine(Startup.WebRootPath, path ?? string.Empty);

            if (!System.IO.File.Exists(path))
                return NotFound();

            System.IO.File.WriteAllBytesAsync(path, fileContents);
            return Ok();
        }
    }
}
