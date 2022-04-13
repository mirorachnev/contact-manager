using ContactManager.Api.Infrastructure;
using ContactManager.MessageBus.Messages.RequestResponses;

namespace ContactManager.Api.Handlers
{
    /// <summary>
    /// Interface to handle response messages
    /// </summary>
    public class HandleResponseBusMessages : IHandleResponseBusMessages
    {
        private readonly IMessageBus _messageBus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageBus">Message bus</param>
        public HandleResponseBusMessages(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        /// inheritdoc        
        public Task Handle(GetResponseMessage message)
        {
            return _messageBus.HandleResponse(message);
        }

        /// inheritdoc 
        public Task Handle(CreateResponseMessage message)
        {
            return _messageBus.HandleResponse(message);
        }

        /// inheritdoc 
        public Task Handle(DeleteResponseMessage message)
        {
            return _messageBus.HandleResponse(message);
        }
    }
}
