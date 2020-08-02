using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Shared.Entities;
using signalr.Hubs;

namespace signalr.Controllers
{
    public class AnnoucementController : Controller
    {
        private IHubContext<MessageHub> _hubContext;
        private readonly IJournalMessageRepository _journalMessageRepository;

        public AnnoucementController(IHubContext<MessageHub> hubContext, IJournalMessageRepository journalMessageRepository)
        {
            _hubContext = hubContext;
            _journalMessageRepository = journalMessageRepository;
        }

        [HttpGet("/announcement")]
        public IActionResult Index()
        {
            var message = new JournalMessage()
            {
                Corpname = "ABC Mart.",
                Title = "message"
            };

            _journalMessageRepository.Create(message);

            return View();
        }

        [HttpPost("/announcement")]
        public async Task<IActionResult> Post([FromForm] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return RedirectToAction("Index");
        }
    }
}