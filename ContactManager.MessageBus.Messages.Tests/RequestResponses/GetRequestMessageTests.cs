﻿using ContactManager.MessageBus.Messages.RequestResponses;
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
            var id = Guid.NewGuid();

            var getRequestMessage = new GetRequestMessage(requestMessageId, id);

            getRequestMessage.RequestMessageId.Should().Be(requestMessageId);
            getRequestMessage.Id.Should().Be(id);
        }

        [Fact]
        public void SerializationTest()
        {
            var requestMessageId = Guid.NewGuid();
            var id = Guid.NewGuid();

            var getRequestMessage = new GetRequestMessage(requestMessageId, id);
            var json = JsonConvert.SerializeObject(getRequestMessage);

            var response = JsonConvert.DeserializeObject<GetRequestMessage>(json);

            response.RequestMessageId.Should().Be(requestMessageId);
            response.Id.Should().Be(id);
        }
    }
}
