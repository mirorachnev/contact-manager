using ContactManager.MessageBus.Messages.DataTypes.Enums;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Update response message
    /// </summary>
    public sealed class UpdateResponseMessage : ResponseMessageBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="errorMessage">Error message, if error</param>
        /// <param name="statusCode">Status code of response</param>
        public UpdateResponseMessage(Guid requestMessageId, string? errorMessage, StatusCode statusCode)
            : base(requestMessageId, errorMessage, statusCode) { }
    }
}
