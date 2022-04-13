using ContactManager.MessageBus.Messages.RequestResponses;
using Rebus.Handlers;

namespace ContactManager.Api.Handlers
{
    /// <summary>
    /// Interface to handle response messages
    /// </summary>
    public interface IHandleResponseBusMessages : 
        IHandleMessages<GetResponseMessage>,
        IHandleMessages<CreateResponseMessage>,
        IHandleMessages<DeleteResponseMessage>
    {
    }
}
