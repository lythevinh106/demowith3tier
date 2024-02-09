using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MydemoFirst.Models.Modules.User.Models;
using MydemoFirst.Services.Contracts;
using MydemoFirst.View.MailLayout;

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMail _mail;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public MailController(IMail mail, IWebHostEnvironment hostingEnvironment, UserManager<User> userManager)
        {
            _mail = mail;
            _hostingEnvironment = hostingEnvironment;




        }

        [HttpGet]
        public async Task<IActionResult> SendMailBasic()
        {


            await _mail.SendMailAsync("day la test mail", "<h1>tieu de test mail<h1>", "lythevinh106@gmail.com");

            return Ok();

        }


        [HttpGet("SendMailBasicWithHtml")]
        public async Task<IActionResult> SendMailBasicWithHtml()
        {

            string fileContent = MailLayoutHelper.GetMailLayout1("ly the vinh 22");

            string jobId = BackgroundJob.Enqueue<IMail>(x => x.SendMailAsync("day la test mail with Html", fileContent, "lythevinh106@gmail.com"));


            return Ok("Send mail successed");

        }
    }
}
