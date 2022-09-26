using Microsoft.AspNetCore.Mvc;

namespace WebSocketsTutorial.Controllers
{
    public class WebSocketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
