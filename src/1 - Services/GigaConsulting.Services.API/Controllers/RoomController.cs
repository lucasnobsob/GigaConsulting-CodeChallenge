using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Services.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GigaConsulting.Services.API.Controllers
{
    public class RoomController : ApiController
    {
        private readonly IRoomAppService _roomAppService;
        private readonly ILogger<ChairController> _logger;

        public RoomController(
            INotificationHandler<DomainNotification> notifications, 
            IRoomAppService roomAppService, 
            ILogger<ChairController> logger,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _roomAppService = roomAppService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoomViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var transaction = await _roomAppService.GetAll();

            return Response(transaction.OrderBy(x => x.Name));
        }
    }
}
