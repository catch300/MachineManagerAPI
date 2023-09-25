using Microsoft.AspNetCore.Mvc;

namespace MachineManagerAPI.Controllers
{
    public class MachineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
