using AutoMapper;
using GigaConsulting.Application.Services;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Repository.EventSourcing;
using GigaConsulting.Tests.FakeData.Chair;
using Moq;
using Xunit;

namespace CredutPay.Tests.Application.Services
{
    public class ChairAppServiceTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IChairRepository> _chairRepositoryMock;
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly Mock<IEventStoreRepository> _eventStoreRepositoryMock;
        private readonly ChairAppService _chairAppService;

        public ChairAppServiceTest()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _eventStoreRepositoryMock = new Mock<IEventStoreRepository>();
            _mapperMock = new Mock<IMapper>();
            _chairRepositoryMock = new Mock<IChairRepository>();

            MockChairMapping();
            _chairAppService = new ChairAppService(
                _mapperMock.Object,
                _chairRepositoryMock.Object,
                _eventStoreRepositoryMock.Object,
                _mediatorHandlerMock.Object
            );
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllChairs()
        {
            // Arrange
            var chairs = new ChairFaker().Generate(10);

            _chairRepositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(chairs);

            // Act
            var result = await _chairAppService.GetAll();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnChairFromId()
        {
            // Arrange
            var chair = new ChairFaker().Generate();

            _chairRepositoryMock.Setup(repo => repo.GetById(chair.Id))
                .ReturnsAsync(chair);

            // Act
            var result = await _chairAppService.GetById(chair.Id);

            // Assert
            Assert.Equal(result.Id, chair.Id);
        }

        [Fact]
        public async Task Register_ShouldSendRegisterCommand()
        {
            // Arrange
            var register = new CreateChairViewModelFaker().Generate();
            var registerCommand = new RegisterNewChairCommand(
                Guid.NewGuid(), 
                register.SerialNumber,
                register.Description,
                register.Model,
                register.Status,
                register.ChairType);

            _mapperMock.Setup(mapper => mapper.Map<RegisterNewChairCommand>(register))
                .Returns(registerCommand);


            // Act
            await _chairAppService.Register(register);

            // Assert
            _mediatorHandlerMock.Verify(mediator => mediator.SendCommand(registerCommand), Times.Once);
        }

        [Fact]
        public async Task GetAllChairHistory_ShouldReturnChairHistory()
        {
            // Arrange
            var aggregateId = Guid.NewGuid();
            var storedEventList = new List<StoredEvent> { new StoredEvent { } };

            _eventStoreRepositoryMock.Setup(repo => repo.All(aggregateId))
                .ReturnsAsync(storedEventList);

            // Act
            var result = await _chairAppService.GetAllHistory(aggregateId);

            // Assert
            Assert.Equal(1, result.Count);
        }


        private void MockChairMapping()
        {
            _mapperMock.Setup(m => m.Map<ChairViewModel>(It.IsAny<Chair>()))
                .Returns((Chair source) => new ChairViewModel
                {
                    Id = source.Id,
                    Description = source.Description,
                    Model = source.Model,
                    SerialNumber = source.SerialNumber,
                    Status = source.Status,
                    Type = source.Type
                });

            _mapperMock.Setup(m => m.Map<List<ChairViewModel>>(It.IsAny<List<Chair>>()))
                .Returns((List<Chair> source) => source.Select(e => new ChairViewModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    Model = e.Model,
                    SerialNumber = e.SerialNumber,
                    Status = e.Status,
                    Type = e.Type
                }).ToList());

            _mapperMock.Setup(mapper => mapper.ConfigurationProvider)
                .Returns(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Chair, ChairViewModel>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                        .ForMember(dest => dest.Model, opt => opt.MapFrom(src => Guid.Parse(src.Model)))
                        .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                        .ReverseMap();
                }));
        }
    }
}
