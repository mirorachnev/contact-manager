using ContactManager.MessageBus.Messages.RequestResponses;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactManager.MessageBus.Messages.Tests.RequestResponses
{
    public class DeleteRequestMessageTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();

            var deleteRequestMessage = new DeleteRequestMessage(requestMessageId, id);

            deleteRequestMessage.RequestMessageId.Should().Be(requestMessageId);
            deleteRequestMessage.Id.Should().Be(id);
        }

        [Fact]
        public void SerializationTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();

            var deleteRequestMessage = new DeleteRequestMessage(requestMessageId, id);
            var json = JsonConvert.SerializeObject(deleteRequestMessage);

            var response = JsonConvert.DeserializeObject<DeleteRequestMessage>(json);

            response.RequestMessageId.Should().Be(requestMessageId);
            response.Id.Should().Be(id);
        }
    }
}
