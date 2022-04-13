using ContactManager.MessageBus.Messages.DataTypes.Enums;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Base class for all responses
    /// </summary>
    public abstract class ResponseMessageBase : MessageBase
    {
        /// <summary>
        /// Id of request message
        /// </summary>
        public Guid RequestMessageId { get; }

        /// <summary>
        /// Error message, if error
        /// </summary>
        public string? ErrorMessage { get; } 

        /// <summary>
        /// Status code of response
        /// </summary>
        public StatusCode StatusCode { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="errorMessage">Error message, if error</param>
        /// <param name="statusCode">Status code of response</param>
        protected ResponseMessageBase(Guid requestMessageId, string? errorMessage, StatusCode statusCode)
        {
            RequestMessageId = requestMessageId;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
