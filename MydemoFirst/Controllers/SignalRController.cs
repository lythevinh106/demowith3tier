using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MydemoFirst.Signal;

namespace MydemoFirst.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SignalRController : Controller
    {
        private readonly IHubContext<MeatHub> _meatHub;

        public SignalRController(IHubContext<MeatHub> meatHub)
        {
            _meatHub = meatHub;
        }

        public IActionResult Demo1()
        {
            return View("Index");
        }

        public IActionResult Demo2()
        {
            return View("Index2");
        }

        public IActionResult Demo3()
        {
            return View("Index3");
        }

        [Route("SignalR/Vote/{type}")]
        public async Task<IActionResult> Vote(string type)
        {
            if (FavoriteMeat.VoteFavoriteMeat.ContainsKey(type))
            {
                FavoriteMeat.VoteFavoriteMeat[type]++;
            }

            await _meatHub.Clients.All.SendAsync("UpdateFavoriteMeat",
                FavoriteMeat.VoteFavoriteMeat[FavoriteMeat.Duck],
                 FavoriteMeat.VoteFavoriteMeat[FavoriteMeat.Cow],

                    FavoriteMeat.VoteFavoriteMeat[FavoriteMeat.Chicken]);

            return Accepted();
        }

        public IActionResult GroupChat()
        {
            return View("Index4");
        }
        public IActionResult Home()
        {
            string htmlContent = System.IO.File.ReadAllText("wwwroot/index1.html");

            // Trả về một ContentResult với kiểu "text/html"
            return Content(htmlContent, "text/html");


        }

    }
}