using Bogus;
using GigaConsulting.Application.ViewModels;

namespace GigaConsulting.Tests.FakeData.Room
{
    public class RoomFaker : Faker<Domain.Models.Room>
    {
        public RoomFaker()
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
        }
    }

    public class RoomViewModelFaker : Faker<RoomViewModel>
    {
        public RoomViewModelFaker()
        {
            RuleFor(x => x.Id, y => Guid.NewGuid());
            RuleFor(x => x.Name, y => y.Lorem.Word());
        }
    }
}
