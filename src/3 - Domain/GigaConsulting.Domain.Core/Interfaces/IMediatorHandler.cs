using GigaConsulting.Domain.Core.Commands;
using GigaConsulting.Domain.Core.Events;

namespace GigaConsulting.Domain.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
