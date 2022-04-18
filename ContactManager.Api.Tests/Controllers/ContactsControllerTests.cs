using ContactManager.Api.Contracts;
using ContactManager.Api.Controllers;
using ContactManager.Api.Infrastructure;
using ContactManager.MessageBus.Messages.DataTypes;
using ContactManager.MessageBus.Messages.RequestResponses;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactManager.Api.Tests.Controllers
{
    public class ContactsControllerTests
    {
        private readonly Mock<IMessageBus> _messageBusMock;
        private readonly ContactsController _contactsController;

        public ContactsControllerTests()
        {
            _messageBusMock = new Mock<IMessageBus>();
            _contactsController = new ContactsController(_messageBusMock.Object);
        }

        [Fact]
        public void GetByIdOkTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();
            var firstName = "first";
            var lastName = "last";
            var email = "test@test.com";
            var phone = "12345";
            var dateOfBirth = DateTime.Now;
            var address = "address";
            var iban = "XXXVVVBNM123";

            var contactData = new List<ContactData> { new ContactData(id, firstName, lastName, email, phone, dateOfBirth, address, iban) };

            var requestMessage = new GetRequestMessage(requestMessageId, id);
            var responseMessage = new GetResponseMessage(requestMessageId, null, MessageBus.Messages.DataTypes.Enums.StatusCode.Ok, contactData);

            _messageBusMock.Setup(x => x.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(It.IsAny<RequestMessageBase>()))
                .Returns<RequestMessageBase>((requestMessage) =>
                {
                    return Task.FromResult(responseMessage);
                });

            var actionResult = _contactsController.Get(id).Result;

            actionResult.Should().NotBeNull();

            var result = ((ObjectResult)actionResult).Value;
            result.Should().NotBeNull();

            result.Should().BeOfType<Contact>();

            var contact = (Contact)result!;

            contact.FirstName.Should().Be(firstName);
            contact.LastName.Should().Be(lastName);
            contact.Email.Should().Be(email);
            contact.PhoneNumber.Should().Be(phone);
            contact.DateOfBirth.Should().Be(dateOfBirth);
            contact.Address.Should().Be(address);
            contact.Iban.Should().Be(iban);
        }

        [Fact]
        public void GetByIdNotFoundTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();

            var requestMessage = new GetRequestMessage(requestMessageId, id);
            var responseMessage = new GetResponseMessage(requestMessageId, null, MessageBus.Messages.DataTypes.Enums.StatusCode.NotFound, null);

            _messageBusMock.Setup(x => x.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(It.IsAny<RequestMessageBase>()))
                .Returns<RequestMessageBase>((requestMessage) =>
                {
                    return Task.FromResult(responseMessage);
                });

            var actionResult = _contactsController.Get(id).Result;

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetByIdErrorTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();

            var requestMessage = new GetRequestMessage(requestMessageId, id);
            var responseMessage = new GetResponseMessage(requestMessageId, "some error", MessageBus.Messages.DataTypes.Enums.StatusCode.Error, null);

            _messageBusMock.Setup(x => x.PublishMessageAndWaitForResponseAsync<GetResponseMessage>(It.IsAny<RequestMessageBase>()))
                .Returns<RequestMessageBase>((requestMessage) =>
                {
                    return Task.FromResult(responseMessage);
                });

            var actionResult = _contactsController.Get(id).Result;

            actionResult.Should().NotBeNull();

            ((ObjectResult)actionResult).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
