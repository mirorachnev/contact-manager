using ContactManager.MessageBus.Messages.RequestResponses;
using Rebus.Handlers;

namespace ContactManager.Api.Handlers
{
    public interface IHandleResponseBusMessages : IHandleMessages<GetResponseMessage>
    {
    }
}
