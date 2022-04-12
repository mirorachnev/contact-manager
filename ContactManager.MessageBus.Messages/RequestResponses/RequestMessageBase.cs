using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Base class for all request messages
    /// </summary>
    public abstract class RequestMessageBase : MessageBase
    {
        /// <summary>
        /// Id of request message
        /// </summary>
        public Guid RequestMessageId { get; }

        /// <summary>
        /// Return address
        /// </summary>
        public string ReturnAddres { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="returnAddress">Return address</param>
        public RequestMessageBase(Guid requestMessageId, string returnAddress)
        {
            RequestMessageId = requestMessageId;
            ReturnAddres = returnAddress;
        }
    }
}
