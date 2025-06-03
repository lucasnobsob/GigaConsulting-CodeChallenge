using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Services.API.Controllers;
using GigaConsulting.Tests.FakeData.Allocation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CredutPay.Tests.Services.API.Controller
{
    public class AllocationControllerTest
    {
        private readonly Mock<DomainNotificationHandler> _notificationHandlerMock;
        private readonly Mock<IAllocationAppService> _allocationAppServiceMock;
        private readonly Mock<ILogger<AllocationController>> _loggerMock;
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly AllocationController _allocationController;

        public AllocationControllerTest()
        {
            _notificationHandlerMock = new Mock<DomainNotificationHandler>();
            _allocationAppServiceMock = new Mock<IAllocationAppService>();
            _loggerMock = new Mock<ILogger<AllocationController>>();
            _mediatorHandlerMock = new Mock<IMediatorHandler>();

            _allocationController = new AllocationController(
                _notificationHandlerMock.Object,
                _allocationAppServiceMock.Object,
                _loggerMock.Object,
                _mediatorHandlerMock.Object
            );
        }

        [Fact]
        public async Task Post_ShouldReturnOkNoContent()
        {
            // Arrange
            var model = new CreateAllocationViewModel();
            _allocationAppServiceMock.Setup(x => x.Register(model)).Returns(Task.CompletedTask);

            // Act
            var result = await _allocationController.Post(model);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest()
        {
            // Arrange
            var model = new CreateAllocationViewModel();
            var notification = new DomainNotification("Name", "The Name field is required.");
            _allocationController.ModelState.AddModelError("Name", "The Name field is required.");
            _notificationHandlerMock.Setup(x => x.HasNotifications()).Returns(true);
            _notificationHandlerMock.Setup(x => x.GetNotifications()).Returns(new List<DomainNotification> { notification });
            _allocationAppServiceMock.Setup(x => x.Register(model)).Returns(Task.CompletedTask);

            // Act
            var result = await _allocationController.Post(model);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResult = Assert.IsType<ErrorResult<object>>(objectResult.Value);
            Assert.False(errorResult.Success);
            Assert.Equal(new[] { "The Name field is required." }, errorResult.Errors);
        }

        [Fact]
        public async Task History_ShouldReturnWalletHistory()
        {
            // Arrange
            var id = Guid.NewGuid();
            var history = new AllocationHistoryFaker().Generate(10);
            _allocationAppServiceMock.Setup(x => x.GetAllHistory(id)).ReturnsAsync(history);

            // Act
            var result = await _allocationController.History(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as SuccessResult<object>;
            Assert.NotNull(data);
            var returnedHistory = Assert.IsAssignableFrom<List<AllocationHistoryData>>(data.Data);
            Assert.Equal(history.Count, returnedHistory.Count);
        }
    }
}
