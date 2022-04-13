using AutoMapper;
using ContactManager.Api.Contracts;
using ContactManager.Api.Infrastructure;
using ContactManager.Common;
using ContactManager.MessageBus.Messages.DataTypes;
using ContactManager.MessageBus.Messages.RequestResponses;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Api.Controllers
{
    /// <summary>
    /// Contacts controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="messageBus">Message bus</param>
        /// <param name="mapper">Automapper</param>
        public ContactsController(ILogger<ContactsController> logger,
            IMessageBus messageBus, IMapper mapper)
        {
            _logger = logger;
            _messageBus = messageBus;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getRequestMessage = new GetRequestMessage(Guid.NewGuid(), Constants.ApiServiceReturnAddress, null);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(getRequestMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            var result = _mapper.Map<IEnumerable<Contact>>(response.Contacts);

            return Ok(result);
        }

        /// <summary>
        /// Get contact by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var getRequestMessage = new GetRequestMessage(Guid.NewGuid(), Constants.ApiServiceReturnAddress, id);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(getRequestMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            if (!response.Contacts!.Any())
                return NotFound();

            var result = _mapper.Map<IEnumerable<Contact>>(response.Contacts?.FirstOrDefault());

            return Ok(result);
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(Contact contact)
        {
            if (contact == null)
                return BadRequest();

            var messageData = _mapper.Map<ContactData>(contact);

            var createMessage = new CreateRequestMessage(Guid.NewGuid(), Constants.ApiServiceReturnAddress, messageData);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<CreateResponseMessage>(createMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            return Ok();
        }
    }
}
