using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OWASP10_2021.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OWASP10_2021.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = OWASP10_2021.Models.User.GetUserFromClaim(User);
            return View(user);
        }

        public async Task<IActionResult> FileManager(string path)
        {
            if (path == "../")
                path = string.Empty;

            if (string.IsNullOrWhiteSpace(path))
            {
                ViewBag.Path = string.Empty;
            }
            else
            {
                ViewBag.Path = path;
            }

            var user = OWASP10_2021.Models.User.GetUserFromClaim(User);
            ViewBag.User = user;

            ViewBag.DefaultPath = Startup.WebRootPath;

            path = Path.Combine(Startup.WebRootPath, path ?? string.Empty);
            if (string.IsNullOrWhiteSpace(ViewBag.Path))
            {
                ViewBag.UpDirectory = Path.GetRelativePath(Startup.WebRootPath, path);
            }

            if (!Directory.Exists(path))
            {
                return NotFound();
            }

            var Dirs = Directory.GetDirectories(path)
                .Select(x => new DirAndFile() { Name = x.Split('\\').Last(), Path = x, Type = "D" });

            var Files = Directory.GetFiles(path)
                .Select(x => new DirAndFile() { Name = x.Split('\\').Last(), Path = x, Type = "F" });

            return View(Dirs.Union(Files));
        }
    }
}
