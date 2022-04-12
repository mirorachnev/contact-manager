using ContactManager.Api.Infrastructure;
using ContactManager.MessageBus.Messages.RequestResponses;

namespace ContactManager.Api.Handlers
{
    public class HandleResponseBusMessages : IHandleResponseBusMessages
    {
        private readonly ILogger<HandleResponseBusMessages> _logger;
        private readonly IMessageBus _messageBus;

        public HandleResponseBusMessages(ILogger<HandleResponseBusMessages> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        public Task Handle(GetResponseMessage message)
        {
            return _messageBus.HandleResponse(message);
        }
    }
}
