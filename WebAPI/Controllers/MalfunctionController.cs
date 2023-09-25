using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class MalfunctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
