using ContactManager.MessageBus.Messages.DataTypes;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Update request message
    /// </summary>
    public sealed class UpdateRequestMessage : RequestMessageBase
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
        public UpdateRequestMessage(Guid requestMessageId, ContactData contactData)
            : base(requestMessageId)
        {
            ContactData = contactData;
        }
    }
}
