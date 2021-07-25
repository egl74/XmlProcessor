using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using XmlProcessorServer.Hubs;

namespace XmlProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HandlerController : ControllerBase
    {
        private IHubContext<HandlerHub> _hubcontext { get; set; }

        public HandlerController(IHubContext<HandlerHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }
    }
}