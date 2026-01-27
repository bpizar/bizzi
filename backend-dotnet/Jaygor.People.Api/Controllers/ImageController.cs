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

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ImageController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet("{type}/{name}")]
        public IActionResult get(string type, string name)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", "images", type, name + ".png");

            if (!System.IO.File.Exists(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", "images", type, "default" + ".png");
            }
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
    }
}