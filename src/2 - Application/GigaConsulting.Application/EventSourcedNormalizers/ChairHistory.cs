using GigaConsulting.Domain.Core.Events;
using System.Text.Json;

namespace GigaConsulting.Application.EventSourcedNormalizers
{
    public class ChairHistory
    {
        public static IList<ChairHistoryData> HistoryData { get; set; }

        public static IList<ChairHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ChairHistoryData>();
            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<ChairHistoryData>();
            var last = new ChairHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ChairHistoryData
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
                var slot = new ChairHistoryData();
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
