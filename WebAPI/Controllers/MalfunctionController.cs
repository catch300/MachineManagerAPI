using Microsoft.AspNetCore.Mvc;

namespace MachineManagerAPI.Controllers
{
    public class MalfunctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
