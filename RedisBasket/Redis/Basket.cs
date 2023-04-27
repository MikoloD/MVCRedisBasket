using Redis.OM.Modeling;
using System.Net;

namespace RedisBasket.Redis
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Basket" })]
    public class Basket
    {
        [RedisIdField]
        [Indexed]
        public string? SessionId { get; set; }

        [Indexed(CascadeDepth = 1)]
        public List<Product> BasketPosition { get; set; } = new List<Product>();

        [Indexed]
        public decimal? CheckoutPrice { get; set; } = 0;

    }
}
