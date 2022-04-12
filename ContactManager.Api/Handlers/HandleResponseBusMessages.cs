using ContactManager.Api.Infrastructure;
using ContactManager.MessageBus.Messages.RequestResponses;

namespace ContactManager.Api.Handlers
{
    /// <summary>
    /// Interface to handle response messages
    /// </summary>
    public class HandleResponseBusMessages : IHandleResponseBusMessages
    {
        private readonly ILogger<HandleResponseBusMessages> _logger;
        private readonly IMessageBus _messageBus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="messageBus">Message bus</param>
        public HandleResponseBusMessages(ILogger<HandleResponseBusMessages> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        /// inheritdoc        
        public Task Handle(GetResponseMessage message)
        {
            return _messageBus.HandleResponse(message);
        }
    }
}
