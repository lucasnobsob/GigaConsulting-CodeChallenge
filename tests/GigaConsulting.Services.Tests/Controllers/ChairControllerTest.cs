using GigaConsulting.Application.EventSourcedNormalizers;
using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Services.API.Controllers;
using GigaConsulting.Tests.FakeData.Chair;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using Xunit;

namespace CredutPay.Tests.Services.API.Controller
{
    public class ChairControllerTest
    {
        private readonly Mock<IChairAppService> _chairAppServiceMock;
        private readonly Mock<DomainNotificationHandler> _notificationHandlerMock;
        private readonly Mock<ILogger<ChairController>> _loggerMock;
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly ChairController _chairController;
        private readonly Guid _userId = Guid.NewGuid();

        public ChairControllerTest()
        {
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userId.ToString())
            }));
            _chairAppServiceMock = new Mock<IChairAppService>();
            _notificationHandlerMock = new Mock<DomainNotificationHandler>();
            _loggerMock = new Mock<ILogger<ChairController>>();
            _mediatorHandlerMock = new Mock<IMediatorHandler>();

            _chairController = new ChairController(
                _notificationHandlerMock.Object,
                _chairAppServiceMock.Object,
                _loggerMock.Object,
                _mediatorHandlerMock.Object
            );

            _chairController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userClaims
                }
            };
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllChairs()
        {
            // Arrange
            var chairs = new ChairViewModelFaker().Generate(10);
            _chairAppServiceMock.Setup(x => x.GetAll()).ReturnsAsync(chairs);

            // Act
            var result = await _chairController.GetAll();

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(result);
            var data = okObject.Value as SuccessResult<object>;
            Assert.NotNull(data);
            var returnedChairs = Assert.IsAssignableFrom<List<ChairViewModel>>(data.Data);
            Assert.Equal(10, returnedChairs.Count);
        }

        [Fact]
        public async Task Post_ShouldReturnOkNoContent()
        {
            // Arrange
            var model = new CreateChairViewModel();
            _chairAppServiceMock.Setup(x => x.Register(model)).Returns(Task.CompletedTask);

            // Act
            var result = await _chairController.Post(model);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest()
        {
            // Arrange
            var model = new CreateChairViewModel();
            var notification = new DomainNotification("Name", "The Name field is required.");
            _chairController.ModelState.AddModelError( "Name", "The Name field is required.");
            _notificationHandlerMock.Setup(x => x.HasNotifications()).Returns(true);
            _notificationHandlerMock.Setup(x => x.GetNotifications()).Returns(new List<DomainNotification> { notification });
            _chairAppServiceMock.Setup(x => x.Register(model)).Returns(Task.CompletedTask);

            // Act
            var result = await _chairController.Post(model);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResult = Assert.IsType<ErrorResult<object>>(objectResult.Value);
            Assert.False(errorResult.Success);
            Assert.Equal(new[] { "The Name field is required." }, errorResult.Errors);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkNoContent()
        {
            // Arrange
            var chairs = new ChairViewModelFaker().Generate(10);
            _chairAppServiceMock.Setup(x => x.Remove(_userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _chairController.Delete(_userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task History_ShouldReturnChairHistory()
        {
            // Arrange
            var history = new ChairHistoryFaker().Generate(10);
            _chairAppServiceMock.Setup(x => x.GetAllHistory(_userId)).ReturnsAsync(history);

            // Act
            var result = await _chairController.History(_userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as SuccessResult<object>;
            Assert.NotNull(data);
            var returnedHistory = Assert.IsAssignableFrom<List<ChairHistoryData>>(data.Data);
            Assert.Equal(history.Count, returnedHistory.Count);
        }
    }
}
