namespace RedisBasket.Services
{
	public class SessionService
	{
        public string? SessionId { get; set; }
		private readonly IHttpContextAccessor _httpContext;
        public SessionService(IHttpContextAccessor httpContext)
        {
			_httpContext = httpContext;
		}
		public void Run()
		{
			SessionId = _httpContext.HttpContext.Session.Id;
		}
    }
}
