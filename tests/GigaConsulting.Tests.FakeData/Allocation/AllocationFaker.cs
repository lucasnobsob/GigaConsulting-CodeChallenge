using Bogus;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Tests.FakeData.Chair;
using GigaConsulting.Tests.FakeData.Room;

namespace GigaConsulting.Tests.FakeData.Allocation
{
    public class AllocationFaker : Faker<Domain.Models.Allocation>
    {
        public AllocationFaker()
        {
            var faker = new Faker();
            var date = faker.Date.Future().Date;
            var fromTime = faker.Date.Between(
                date.AddHours(8),
                date.AddHours(16)
            );

            var toTime = faker.Date.Between(
                fromTime.AddMinutes(1),
                date.AddHours(22)
            );
            RuleFor(x => x.From, fromTime);
            RuleFor(x => x.To, toTime);
            RuleFor(x => x.Chair, new ChairFaker().Generate());
            RuleFor(x => x.Room, new RoomFaker().Generate());
        }
    }

    public class AllocationViewModelFaker : Faker<AllocationViewModel>
    {
        public AllocationViewModelFaker()
        {
            var faker = new Faker();
            var date = faker.Date.Future().Date;
            var fromTime = faker.Date.Between(
                date.AddHours(8),
                date.AddHours(16)
            );

            var toTime = faker.Date.Between(
                fromTime.AddMinutes(1),
                date.AddHours(22)
            );
            RuleFor(x => x.From, fromTime);
            RuleFor(x => x.To, toTime);
            RuleFor(x => x.Chair, new ChairViewModelFaker().Generate());
            RuleFor(x => x.Room, new RoomViewModelFaker().Generate());
        }
    }

    public class CreateAllocationViewModelFaker : Faker<CreateAllocationViewModel>
    {
        public CreateAllocationViewModelFaker()
        {
            var faker = new Faker();
            var date = faker.Date.Future().Date;
            var fromTime = faker.Date.Between(
                date.AddHours(8),
                date.AddHours(16)
            );

            var toTime = faker.Date.Between(
                fromTime.AddMinutes(1),
                date.AddHours(22)
            );
            RuleFor(x => x.From, fromTime);
            RuleFor(x => x.To, toTime);
            RuleFor(x => x.ChairId, Guid.NewGuid());
            RuleFor(x => x.RoomId, Guid.NewGuid());
        }
    }
}
