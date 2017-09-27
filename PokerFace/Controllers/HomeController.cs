namespace PokerFace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return this.View();
        }
    }
}