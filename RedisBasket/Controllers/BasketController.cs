using Microsoft.AspNetCore.Mvc;
using Redis.OM.Searching;
using Redis.OM;
using System;
using RedisBasket.Redis;
using System.Linq;
using RedisBasket.Models;
using RedisBasket.Services;
using System.Collections.Generic;

namespace RedisBasket.Controllers
{
	public class BasketController : Controller
	{
		private readonly RedisCollection<Basket> _baskets;
		private readonly Basket _basket;
		private readonly RedisConnectionProvider _provider;
		private readonly string _sessionId;
		public BasketController(RedisConnectionProvider provider, SessionService sessionService)
		{
			if (sessionService.SessionId == null) sessionService.Run();
			_sessionId = sessionService.SessionId;
			_provider = provider;
			_baskets = (RedisCollection<Basket>)provider.RedisCollection<Basket>();
			_basket = _baskets.FirstOrDefault(x => x.SessionId == _sessionId);
			if (_basket == null)
			{
				_basket = new Basket() { SessionId = _sessionId };
				_baskets.Insert(_basket);
			}
			else
			{
				_basket.BasketPosition = _basket.BasketPosition.GroupBy(x => x.ProductId)
					.Select(g => g.OrderByDescending(x => x.Quantity).First()).ToList();
			}

		}
		public IActionResult Index()
		{
			BasketViewModel model = new BasketViewModel();
			model.Basket = _basket;
			return View(model);
		}
		[HttpPost]
		public IActionResult AddBasketPosition(BasketViewModel basketViewModel)
		{
			var product = basketViewModel.AddProduct;
			if (product.ProductId != null)
			{
				var myBasket = _basket.BasketPosition.FirstOrDefault(x => x.ProductId == product.ProductId);
				if (myBasket == null)
				{
					_basket.BasketPosition.Add(product);
				}
				else
				{
					myBasket.Quantity += product.Quantity;
				}
				_basket.CheckoutPrice += product.Price * product.Quantity;
				_baskets.Save();

			}
			return RedirectToAction(nameof(Index));
		}
	}
}
