using AutoMapper;
using GigaConsulting.Application.Services;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Repository.EventSourcing;
using GigaConsulting.Tests.FakeData.Allocation;
using Moq;

namespace CredutPay.Tests.Application.Services
{
    public class AllocationAppServiceTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAllocationRepository> _allocationRepositoryMock;
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly Mock<IEventStoreRepository> _eventStoreRepositoryMock;
        private readonly AllocationAppService _allocationAppService;

        public AllocationAppServiceTest()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _eventStoreRepositoryMock = new Mock<IEventStoreRepository>();
            _mapperMock = new Mock<IMapper>();
            _allocationRepositoryMock = new Mock<IAllocationRepository>();

            MockAllocationMapping();
            _allocationAppService = new AllocationAppService(
                _eventStoreRepositoryMock.Object,
                _allocationRepositoryMock.Object,
                _mediatorHandlerMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAllocations()
        {
            // Arrange
            var allocations = new AllocationFaker().Generate(10);

            _allocationRepositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(allocations);

            // Act
            var result = await _allocationAppService.GetAll();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task Register_ShouldSendRegisterCommand()
        {
            // Arrange
            var register = new CreateAllocationViewModelFaker().Generate();
            var registerCommand = new RegisterNewAllocationCommand(
                register.From, 
                register.To, 
                register.RoomId, 
                register.ChairId);

            _mapperMock.Setup(mapper => mapper.Map<RegisterNewAllocationCommand>(register))
                .Returns(registerCommand);


            // Act
            await _allocationAppService.Register(register);

            // Assert
            _mediatorHandlerMock.Verify(mediator => mediator.SendCommand(registerCommand), Times.Once);
        }

        [Fact]
        public async Task GetAllAllocationHistory_ShouldReturnAllocationHistory()
        {
            // Arrange
            var aggregateId = Guid.NewGuid();
            var storedEventList = new List<StoredEvent> { new StoredEvent { } };

            _eventStoreRepositoryMock.Setup(repo => repo.All(aggregateId))
                .ReturnsAsync(storedEventList);

            // Act
            var result = await _allocationAppService.GetAllHistory(aggregateId);

            // Assert
            Assert.Single(result);
        }

        private void MockAllocationMapping()
        {
            _mapperMock.Setup(m => m.Map<List<AllocationViewModel>>(It.IsAny<List<Allocation>>()))
                .Returns((List<Allocation> source) => source.Select(e => new AllocationViewModel
                {
                    From = e.From,
                    To = e.To,
                    Chair = new ChairViewModel
                    {
                        Id = e.Chair.Id,
                        Description = e.Chair.Description,
                        Model = e.Chair.Model,
                        SerialNumber = e.Chair.SerialNumber,
                        Status = e.Chair.Status,
                        Type = e.Chair.Type
                    },
                    Room = new RoomViewModel
                    {
                        Id = e.Room.Id,
                        Name = e.Room.Name,
                    }
                }).ToList());

            _mapperMock.Setup(mapper => mapper.ConfigurationProvider)
                .Returns(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Allocation, AllocationViewModel>().ReverseMap();

                    cfg.CreateMap<IEnumerable<Allocation>, IEnumerable<AllocationViewModel>>().ReverseMap();

                    cfg.CreateMap<Allocation, CreateAllocationViewModel>().ReverseMap();
                }));
        }
    }
}
