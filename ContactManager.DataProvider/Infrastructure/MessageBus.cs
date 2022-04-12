using AutoMapper;
using ContactManager.Common;
using ContactManager.DataProvider.Repositories;
using ContactManager.MessageBus.Messages.DataTypes;
using ContactManager.MessageBus.Messages.DataTypes.Enums;
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
        private readonly IMapper _mapper;
        private readonly IContactsRepository _contactsRepository;
        private IBus? _bus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="contactsRepository">Contacts repository</param>
        public MessageBus(ILogger<MessageBus> logger, IMapper mapper, IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
            _logger = logger;
            _connectionString = Constants.RabbitMqConnectionString;
            _mapper = mapper;

            _activator = new BuiltinHandlerActivator();
            _activator.Handle<RequestMessageBase>(x => HandleRequest(x));
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

        private async Task HandleRequest(RequestMessageBase requestMessage)
        {
            if (requestMessage is GetRequestMessage getRequestMessage)
            {
                await HandleGetRequest(getRequestMessage);
            }
        }

        private async Task HandleGetRequest(GetRequestMessage requestMessage)
        {
            try
            {
                var result = new List<ContactData>();
                GetResponseMessage? response = default;

                switch (requestMessage.GetReuestType)
                {
                    case GetRequestType.ById:
                        var contact = await _contactsRepository.GetAsync((Guid)requestMessage.Id!);

                        if (contact == null)
                        {
                            response = new GetResponseMessage(requestMessage.RequestMessageId, null, StatusCode.NotFound, null);
                        }
                        else
                        {
                            result.Add(_mapper.Map<ContactData>(contact));
                            response = new GetResponseMessage(requestMessage.RequestMessageId, null, StatusCode.Ok, result);
                        }

                        break;
                    case GetRequestType.ByQuery:
                        var contacts = await _contactsRepository.GetAsync(requestMessage.Query);
                        result.AddRange(_mapper.Map<IEnumerable<ContactData>>(contacts));
                        response = new GetResponseMessage(requestMessage.RequestMessageId, null, StatusCode.Ok, result);
                        break;
                    default:
                        break;
                }

                await _bus!.Reply(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new GetResponseMessage(requestMessage.RequestMessageId, ex.Message, StatusCode.Error, null);
                await _bus!.Reply(response);
            }
        }
    }
}
