using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.StaticFiles;

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class FileController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet("{type}/{name}")]
        public IActionResult get(string type, string name)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", "files", type, name);

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var image = System.IO.File.OpenRead(path);
            return File(image, contentType);
        }
    }
}