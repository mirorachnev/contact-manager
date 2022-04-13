using ContactManager.MessageBus.Messages.DataTypes.Enums;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Delete response message
    /// </summary>
    public sealed class DeleteResponseMessage : ResponseMessageBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="errorMessage">Error message, if error</param>
        /// <param name="statusCode">Status code of response</param>
        public DeleteResponseMessage(Guid requestMessageId, string? errorMessage, StatusCode statusCode)
            : base(requestMessageId, errorMessage, statusCode) { }
    }
}
