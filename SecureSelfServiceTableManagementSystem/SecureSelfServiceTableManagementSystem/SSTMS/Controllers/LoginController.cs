using Microsoft.AspNetCore.Mvc;

namespace SSTMS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
