using ContactManager.MessageBus.Messages.DataTypes;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Create request message
    /// </summary>
    public sealed class CreateRequestMessage : RequestMessageBase
    {
        /// <summary>
        /// Contact to be created
        /// </summary>
        public ContactData ContactData { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Request message id</param>
        /// <param name="contactData">Contact</param>
        public CreateRequestMessage(Guid requestMessageId, ContactData contactData)
            : base(requestMessageId)
        {
            ContactData = contactData;
        }
    }
}
