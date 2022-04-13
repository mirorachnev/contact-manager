using ContactManager.MessageBus.Messages.RequestResponses;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using Xunit;

namespace ContactManager.MessageBus.Messages.Tests.RequestResponses
{
    public class GetRequestMessageTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var requestMessageId = Guid.NewGuid();
            var returnAddress = "address";
            var id = Guid.NewGuid();

            var getRequestMessage = new GetRequestMessage(requestMessageId, returnAddress, id);

            getRequestMessage.RequestMessageId.Should().Be(requestMessageId);
            getRequestMessage.ReturnAddres.Should().Be(returnAddress);
            getRequestMessage.Id.Should().Be(id);
        }

        [Fact]
        public void SerializationTest()
        {
            var requestMessageId = Guid.NewGuid();
            var returnAddress = "address";
            var id = Guid.NewGuid();

            var getRequestMessage = new GetRequestMessage(requestMessageId, returnAddress, id);
            var json = JsonConvert.SerializeObject(getRequestMessage);

            var response = JsonConvert.DeserializeObject<GetRequestMessage>(json);

            response.RequestMessageId.Should().Be(requestMessageId);
            response.ReturnAddres.Should().Be(returnAddress);
            response.Id.Should().Be(id);
        }
    }
}
