using Microsoft.AspNetCore.Mvc;

namespace TP_WikY_API.Controllers
{
	public class ArticleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
