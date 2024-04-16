using Microsoft.AspNetCore.Mvc;

namespace TP_WikY_API.Controllers
{
	public class ThemeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
