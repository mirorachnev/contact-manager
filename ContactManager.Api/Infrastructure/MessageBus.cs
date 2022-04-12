using ContactManager.Api.Handlers;
using ContactManager.Common;
using ContactManager.MessageBus.Messages.RequestResponses;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;

namespace ContactManager.Api.Infrastructure
{
    /// <summary>
    /// Message Bus
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private readonly ILogger<MessageBus> _logger;

        private readonly string _connectionString;
        private readonly BuiltinHandlerActivator _activator;
        private readonly IBus _bus;

        private event EventHandler<MessageBase>? ResponseReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="serviceProvider">Service provider</param>
        public MessageBus(ILogger<MessageBus> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _connectionString = Constants.RabbitMqConnectionString;

            _activator = new BuiltinHandlerActivator();
            _activator.Register(serviceProvider.GetService<IHandleResponseBusMessages>);

            _bus = Configure.With(_activator)
                .Options(o =>
                {
                    o.SetNumberOfWorkers(3);
                    o.SetMaxParallelism(15);
                })
                .Transport(t => t.UseRabbitMq(_connectionString, Constants.RabbitMqQueueName))
                .Start();

            _bus.Subscribe<GetResponseMessage>().Wait();
        }

        /// inheritdoc
        public void Dispose()
        {
            _bus.Dispose();
            _activator.Dispose();
        }

        /// inheritdoc
        public Task HandleResponse(MessageBase responseMessage)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    ResponseReceived?.Invoke(this, responseMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                }
            });
        }

        /// inheritdoc
        public async Task<TResponse> PublishMessageAndWaitForResponseAsync<TResponse>(MessageBase requestMessage) where TResponse : MessageBase
        {
            // Define local response received event.
            using var responseReceived = new ManualResetEvent(false);
            TResponse? response = default;

            // Create local function that checks if received response is of current request.
            void ResponseReceivedCallback(object? sender, MessageBase? responseMessage)
            {
                if (responseMessage == null)
                    return;

                try
                {
                    // Assign response to a variable and signal that the response was received.

                    if (responseMessage is ResponseMessageBase responseMessageBase)
                    {
                        response = responseMessage as TResponse;
                        responseReceived?.Set();
                    }
                    
                }
                catch (Exception)
                {
                    _logger.LogWarning($"Exception while handling response {responseMessage.GetType().Name}");
                }
            }

            // Subscribe to event handler of received responses.
            ResponseReceived += ResponseReceivedCallback;

            try
            {
                // Check and adjust timeout timespan.
                var timeout = TimeSpan.FromSeconds(20);

                // Publish request on message bus.
                await _bus.Publish(requestMessage);

                // Wait for the response to be received.
                if (!responseReceived.WaitOne(timeout))
                    throw new TimeoutException($"Response for request  was not received before timeout occured");

                return response!;
            }
            finally
            {
                // Unsubscribe from the event.
                ResponseReceived -= ResponseReceivedCallback;
            }
        }
    }
}
