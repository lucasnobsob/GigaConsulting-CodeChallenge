using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.Interfaces;
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
    public class ChairController : ApiController
    {
        private readonly IChairAppService _chairAppService;
        private readonly ILogger<ChairController> _logger;

        public ChairController(
            INotificationHandler<DomainNotification> notifications, 
            IChairAppService chairAppService, 
            ILogger<ChairController> logger,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _chairAppService = chairAppService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChairViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var transaction = await _chairAppService.GetAll();

            return Response(transaction);
        }

        [HttpPost]
        [Authorize(Policy = "CanCreateChairData", Roles = Roles.Admin)]
        [ProducesResponseType(typeof(CreateChairViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateChairViewModel chairCreateViewModel)
        {
            _logger.LogInformation("Objeto recebido: {@chairCreateViewModel}", chairCreateViewModel);

            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(chairCreateViewModel);
            }

            await _chairAppService.Register(chairCreateViewModel);

            return Created();
        }

        [HttpPost]
        [Authorize(Policy = "CanUpdateChairData", Roles = Roles.Admin)]
        [ProducesResponseType(typeof(UpdateChairViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateChairViewModel chairUpdateViewModel)
        {
            _logger.LogInformation("Objeto recebido: {@chairUpdateViewModel}", chairUpdateViewModel);

            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(chairUpdateViewModel);
            }

            await _chairAppService.Update(chairUpdateViewModel);

            return Accepted();
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveChairData", Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ChairViewModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Guid recebido: {@guid}", id);

            await _chairAppService.Remove(id);

            return NoContent();
        }

        [HttpGet]
        [Route("history/{id:guid}")]
        [ProducesResponseType(typeof(IList<ChairHistoryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> History(Guid id)
        {
            var walletHistoryData = await _chairAppService.GetAllHistory(id);
            return Response(walletHistoryData);
        }
    }
}
