using Microsoft.AspNetCore.Mvc;
using RedisBasket.Services;

namespace RedisBasket.Controllers
{
	public class NewSessionController : Controller
	{
		private SessionService _sessionService;
		public NewSessionController(SessionService SessionService)
        {
			_sessionService = SessionService;
		}
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult Create()
		{
			_sessionService.Run();
			return RedirectToAction(nameof(Index));
		}
	}
}
