using ContactManager.MessageBus.Messages.DataTypes;
using ContactManager.MessageBus.Messages.DataTypes.Enums;
using System;
using System.Collections.Generic;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Response to get request
    /// </summary>
    public class GetResponseMessage : ResponseMessageBase
    {
        /// <summary>
        /// Contact data to be returned
        /// </summary>
        public IEnumerable<ContactData> Contacts { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="errorMessage">Error message</param>
        /// <param name="statusCode">Status code</param>
        /// <param name="contacts">Contact data</param>
        public GetResponseMessage(Guid requestMessageId, string? errorMessage, StatusCode statusCode, IEnumerable<ContactData> contacts)
            : base(requestMessageId, errorMessage, statusCode)
        {
            Contacts = contacts;
        }
    }
}
