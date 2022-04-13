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
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        protected RequestMessageBase(Guid requestMessageId)
        {
            RequestMessageId = requestMessageId;
        }
    }
}
