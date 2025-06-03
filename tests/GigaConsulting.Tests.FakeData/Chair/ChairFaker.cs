using Bogus;
using GigaConsulting.Application.ViewModels;
using GigaConsulting.Domain.Models.Enums;

namespace GigaConsulting.Tests.FakeData.Chair
{
    public class ChairFaker : Faker<Domain.Models.Chair>
    {
        public ChairFaker()
        {
            RuleFor(x => x.SerialNumber, y => Guid.NewGuid().ToString());
            RuleFor(x => x.Description, y => y.Lorem.Sentence(10));
            RuleFor(x => x.Model, y => y.Lorem.Word());
            RuleFor(x => x.Status, y => y.PickRandom<Status>());
            RuleFor(x => x.Type, y => y.PickRandom<ChairType>());
        }
    }

    public class ChairViewModelFaker : Faker<ChairViewModel>
    {
        public ChairViewModelFaker()
        {
            RuleFor(x => x.SerialNumber, y => Guid.NewGuid().ToString());
            RuleFor(x => x.Description, y => y.Lorem.Sentence(10));
            RuleFor(x => x.Model, y => y.Lorem.Word());
            RuleFor(x => x.Status, y => y.PickRandom<Status>());
            RuleFor(x => x.Type, y => y.PickRandom<ChairType>());
        }
    }

    public class CreateChairViewModelFaker : Faker<CreateChairViewModel>
    {
        public CreateChairViewModelFaker()
        {
            RuleFor(x => x.SerialNumber, y => Guid.NewGuid().ToString());
            RuleFor(x => x.Description, y => y.Lorem.Sentence(10));
            RuleFor(x => x.Model, y => y.Lorem.Word());
            RuleFor(x => x.Status, y => y.PickRandom<Status>());
            RuleFor(x => x.ChairType, y => y.PickRandom<ChairType>());
        }
    }
}
