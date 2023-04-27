using Redis.OM.Modeling;
using System.ComponentModel.DataAnnotations;

namespace RedisBasket.Redis
{
    public class Product
    {
        [Indexed]
		[Required(ErrorMessage = "Please enter product id")]
		public int? ProductId { get; set; }

		[Required(ErrorMessage = "Please enter product name")]
		[Indexed]
        public string? ProductName { get; set; }

		[Required(ErrorMessage = "Please enter product quantity")]
		[Indexed]
        public int? Quantity { get; set; }

		[Required(ErrorMessage = "Please enter product price")]
		[Indexed]
        public decimal? Price { get; set; }
    }
}
