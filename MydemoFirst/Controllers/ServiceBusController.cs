using DTOShared.Enums;
using Microsoft.AspNetCore.Mvc;

using MydemoFirst.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusController : ControllerBase
    {
        private readonly IServiceBus<Services.StorageServices.Message> _serviceBusMessage;

        public ServiceBusController(IServiceBus<Services.StorageServices.Message> serviceBusMessage)
        {

            _serviceBusMessage = serviceBusMessage;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public ActionResult Post([FromBody] Services.StorageServices.Message message)
        {

            _serviceBusMessage.Send(TopicsAzureEnum.meesagetopic.ToString(), message,

                new Dictionary<string, string>
                {

                }
                );

            return Ok();

        }


    }
}
