using BusinessLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IUserBL userBL;
        public TicketController(IBus bus, IUserBL userBL)
        {
            this._bus = bus;
            this.userBL = userBL;
        }
        
        
        
    }
}
