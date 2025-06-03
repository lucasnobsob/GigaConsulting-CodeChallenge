using GigaConsulting.Domain.Core.Events;
using System.Text.Json;

namespace GigaConsulting.Application.EventSourcedNormalizers
{
    public class AllocationHistory
    {
        public static IList<AllocationHistoryData> HistoryData { get; set; }

        public static IList<AllocationHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<AllocationHistoryData>();
            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<AllocationHistoryData>();
            var last = new AllocationHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new AllocationHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    UserName = string.IsNullOrWhiteSpace(change.UserName) || change.UserName == last.UserName
                        ? ""
                        : change.UserName,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new AllocationHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CustomerRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.UserName = values["UserName"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerUpdatedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.UserName = values["UserName"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}
