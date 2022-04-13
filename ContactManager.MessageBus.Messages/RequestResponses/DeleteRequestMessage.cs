using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Delete request message
    /// </summary>
    public sealed class DeleteRequestMessage : RequestMessageBase
    {
        /// <summary>
        /// Id of contact to be deleted
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Request message id</param>
        /// <param name="returnAddress">Return address</param>
        /// <param name="id">Id</param>
        public DeleteRequestMessage(Guid requestMessageId, string returnAddress, Guid id)
            : base(requestMessageId, returnAddress)
        {
            Id = id;
        }
    }
}
