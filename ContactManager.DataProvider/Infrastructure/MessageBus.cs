using ContactManager.Common;
using ContactManager.MessageBus.Messages.RequestResponses;
using Microsoft.Extensions.Logging;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.DataProvider.Infrastructure
{
    /// <summary>
    /// Message Bus
    /// </summary>
    internal class MessageBus : IMessageBus
    {
        private readonly ILogger<MessageBus> _logger;
        private readonly string _connectionString;
        private readonly BuiltinHandlerActivator _activator;
        private IBus? _bus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        public MessageBus(ILogger<MessageBus> logger)
        {
            _logger = logger;
            _connectionString = Constants.RabbitMqConnectionString;

            _activator = new BuiltinHandlerActivator();
            _activator.Handle<GetRequestMessage>(x => HandleRequest(x));
        }

        /// <inheritdoc/>
        public async Task ConnectAsync()
        {
            _bus = Configure.With(_activator)
                .Options(o =>
                {
                    o.SetNumberOfWorkers(3);
                    o.SetMaxParallelism(15);
                })
                .Transport(t => t.UseRabbitMq(_connectionString, Constants.RabbitMqQueueName))
                .Start();

            await _bus.Subscribe<GetRequestMessage>();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _bus?.Dispose();
            _activator.Dispose();
        }

        private Task HandleRequest(GetRequestMessage requestMessage)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    // To do 
                    // Handle request here
                    //_bus?.Reply(new GetResponseMessage(requestMessage.Content + "response")).Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                }
            });
        }
    }
}
