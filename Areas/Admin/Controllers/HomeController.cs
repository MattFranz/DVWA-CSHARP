using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OWASP10_2021.Areas.Admin.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OWASP10_2021.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        string baseDirectory = string.Empty;
        public HomeController(IWebHostEnvironment env)
        {
            baseDirectory = env.WebRootPath;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FileManager(string path)
        {
            var result = new DirAndFiles(); ;
            if (!string.IsNullOrWhiteSpace(path))
            {
                path = baseDirectory;
            }
            else
            {
                path = Path.Combine(baseDirectory, path);
            }

            if (!Directory.Exists(path))
                return View(result);

            try
            {
                var file = new FileInfo(path);
                if (file != null)
                {
                    result.FileContents = System.IO.File.ReadAllBytes(path);
                }
            }
            catch
            {
            }
            
            result.Directories = Directory.GetDirectories(path).ToList() ?? new List<string>();
            result.Files = Directory.GetFiles(path).ToList() ?? new List<string>();

            return View(result);
        }
    }
}
