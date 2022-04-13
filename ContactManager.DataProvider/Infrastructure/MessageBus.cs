using AutoMapper;
using ContactManager.Common;
using ContactManager.DataProvider.DbData;
using ContactManager.DataProvider.Repositories;
using ContactManager.MessageBus.Messages.DataTypes;
using ContactManager.MessageBus.Messages.DataTypes.Enums;
using ContactManager.MessageBus.Messages.RequestResponses;
using Microsoft.Extensions.Logging;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;

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
        /// <param name="contactsRepository">Contacts repository</param>
        public MessageBus(ILogger<MessageBus> logger, IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
            _logger = logger;
            _connectionString = Constants.RabbitMqConnectionString;
            _mapper = Utilities.AutoMapper.GetMapper();

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

            // Subscribe to messages
            await _bus.Subscribe<GetRequestMessage>();
            await _bus.Subscribe<CreateRequestMessage>();
            await _bus.Subscribe<DeleteRequestMessage>();
            await _bus.Subscribe<UpdateRequestMessage>();
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

            if (requestMessage is CreateRequestMessage createRequestMessage)
            {
                await HandleCreateRequest(createRequestMessage);
            }

            if (requestMessage is DeleteRequestMessage deleteRequestMessage)
            {
                await HandleDeleteRequest(deleteRequestMessage);
            }

            if (requestMessage is UpdateRequestMessage updateRequestMessage)
            {
                await HandleUpdateRequest(updateRequestMessage);
            }
        }

        private async Task HandleUpdateRequest(UpdateRequestMessage updateRequestMessage)
        {
            UpdateResponseMessage? updateResponseMessage;
            try
            {
                var dbContact = _mapper.Map<Contact>(updateRequestMessage.ContactData);
                var result = await _contactsRepository.UpdateAsync(dbContact);

                updateResponseMessage = new UpdateResponseMessage(updateRequestMessage.RequestMessageId, null, result ? StatusCode.Ok : StatusCode.NotFound);
                await _bus!.Reply(updateResponseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                updateResponseMessage = new UpdateResponseMessage(updateRequestMessage.RequestMessageId, ex.Message, StatusCode.Error);
                await _bus!.Reply(updateResponseMessage);
            }
        }

        private async Task HandleDeleteRequest(DeleteRequestMessage deleteRequestMessage)
        {
            DeleteResponseMessage? deleteResponseMessage;
            try
            {
                var result = await _contactsRepository.DeleteAsync(deleteRequestMessage.Id);

                deleteResponseMessage = new DeleteResponseMessage(deleteRequestMessage.RequestMessageId, null, result ? StatusCode.Ok : StatusCode.NotFound);
                
                await _bus!.Reply(deleteResponseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                deleteResponseMessage = new DeleteResponseMessage(deleteRequestMessage.RequestMessageId, ex.Message, StatusCode.Error);
                await _bus!.Reply(deleteResponseMessage);
            }
        }

        private async Task HandleCreateRequest(CreateRequestMessage createRequestMessage)
        {
            CreateResponseMessage? createResponseMessage;
            try
            {
                var dbContact = _mapper.Map<Contact>(createRequestMessage.ContactData);
                await _contactsRepository.CreateAsync(dbContact);

                createResponseMessage = new CreateResponseMessage(createRequestMessage.RequestMessageId, null, StatusCode.Ok);
                await _bus!.Reply(createResponseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                createResponseMessage = new CreateResponseMessage(createRequestMessage.RequestMessageId, ex.Message, StatusCode.Error);
                await _bus!.Reply(createResponseMessage);
            }
        }

        private async Task HandleGetRequest(GetRequestMessage getRequestMessage)
        {
            if (getRequestMessage.Id == null)
            {
                await HandleGetAllRequest(getRequestMessage);
            }
            else
            {
                await HandleGetByIdRequest(getRequestMessage);
            }
        }

        private async Task HandleGetByIdRequest(GetRequestMessage getRequestMessage)
        {
            GetResponseMessage? responseMessage;
            try
            {
                var contact = await _contactsRepository.GetAsync((Guid)getRequestMessage.Id!);

                if (contact == null)
                {
                    responseMessage = new GetResponseMessage(getRequestMessage.RequestMessageId, null, StatusCode.NotFound, null);                    
                }
                else
                {
                    var messageBusData = _mapper.Map<ContactData>(contact);
                    responseMessage = new GetResponseMessage(getRequestMessage.RequestMessageId, null, StatusCode.Ok, new List<ContactData> { messageBusData });
                }                

                await _bus!.Reply(responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                responseMessage = new GetResponseMessage(getRequestMessage.RequestMessageId, ex.Message, StatusCode.Error, null);
                await _bus!.Reply(responseMessage);
            }
        }

        private async Task HandleGetAllRequest(GetRequestMessage getRequestMessage)
        {
            GetResponseMessage? responseMessage;
            try
            {
                var contacts = await _contactsRepository.GetAsync();

                var contactsData = _mapper.Map<IEnumerable<ContactData>>(contacts);

                responseMessage = new GetResponseMessage(getRequestMessage.RequestMessageId, null, StatusCode.Ok, contactsData);
                await _bus!.Reply(responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                responseMessage = new GetResponseMessage(getRequestMessage.RequestMessageId, ex.Message, StatusCode.Error, null);
                await _bus!.Reply(responseMessage);
            }
        }
    }
}
