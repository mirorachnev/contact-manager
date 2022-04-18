using AutoMapper;
using ContactManager.Api.Contracts;
using ContactManager.Api.Infrastructure;
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
        private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageBus">Message bus</param>
        public ContactsController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _mapper = Utilities.AutoMapper.GetMapper();
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getRequestMessage = new GetRequestMessage(Guid.NewGuid(), null);

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
            var getRequestMessage = new GetRequestMessage(Guid.NewGuid(), id);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(getRequestMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.NotFound)
                return NotFound();

            var result = _mapper.Map<Contact>(response.Contacts?.FirstOrDefault());

            return Ok(result);
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ContactBase contact)
        {
            if (contact == null)
                return BadRequest();

            var messageData = _mapper.Map<ContactData>(contact);

            var createMessage = new CreateRequestMessage(Guid.NewGuid(), messageData);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<CreateResponseMessage>(createMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            return Ok();
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequestMessage = new DeleteRequestMessage(Guid.NewGuid(), id);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<DeleteResponseMessage>(deleteRequestMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.NotFound)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="contact">Contact</param>
        [HttpPut]
        public async Task<IActionResult> Put(Contact contact)
        {
            if (contact == null)
                return BadRequest();

            var messageData = _mapper.Map<ContactData>(contact);

            var updateRequestMessage = new UpdateRequestMessage(Guid.NewGuid(), messageData);

            var response = await _messageBus.PublishMessageAndWaitForResponseAsync<UpdateResponseMessage>(updateRequestMessage);

            if (response is null)
                return new ObjectResult("Response was not received.") { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.Error)
                return new ObjectResult(response.ErrorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

            if (response.StatusCode == MessageBus.Messages.DataTypes.Enums.StatusCode.NotFound)
                return NotFound();

            return Ok();
        }
    }
}
