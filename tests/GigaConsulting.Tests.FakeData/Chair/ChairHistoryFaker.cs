using Bogus;
using GigaConsulting.Application.EventSourcedNormalizers;

namespace GigaConsulting.Tests.FakeData.Chair
{
    public class ChairHistoryFaker : Faker<ChairHistoryData>
    {
        public ChairHistoryFaker()
        {
            RuleFor(x => x.Id, y => Guid.NewGuid().ToString());
            RuleFor(x => x.UserName, y => y.Name.FirstName());
            RuleFor(x => x.When, y => y.Date.Between(DateTime.MinValue, DateTime.Now).ToString());
            RuleFor(x => x.Who, y => Guid.NewGuid().ToString());
        }
    }
}
