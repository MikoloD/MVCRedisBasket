using RedisBasket.Redis;

namespace RedisBasket.Models
{
    public class BasketViewModel
    {
        public Basket Basket { get; set; }
        public Product AddProduct { get; set; }
    }
}
