using Microsoft.AspNetCore.Mvc;

namespace SkillSystem.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
