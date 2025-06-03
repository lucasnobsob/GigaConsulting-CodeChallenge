using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.Services;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Infra.CrossCutting.Identity.Authorization;
using GigaConsulting.Services.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GigaConsulting.Services.API.Controllers
{
    public class AllocationController : ApiController
    {
        private readonly IAllocationAppService _allocationAppService;
        private readonly ILogger<AllocationController> _logger;

        public AllocationController(
            INotificationHandler<DomainNotification> notifications, 
            IAllocationAppService allocationAppService, 
            ILogger<AllocationController> logger,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _allocationAppService = allocationAppService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "CanCreateAllocationData", Roles = Roles.Admin)]
        [ProducesResponseType(typeof(CreateAllocationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateAllocationViewModel allocationCreateViewModel)
        {
            _logger.LogInformation("Objeto recebido: {@allocationCreateViewModel}", allocationCreateViewModel);

            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(allocationCreateViewModel);
            }

            await _allocationAppService.Register(allocationCreateViewModel);

            return Created();
        }

        [HttpGet]
        [Route("history/{id:guid}")]
        [ProducesResponseType(typeof(IList<AllocationHistoryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> History(Guid id)
        {
            var allocationHistoryData = await _allocationAppService.GetAllHistory(id);
            return Response(allocationHistoryData);
        }
    }
}
