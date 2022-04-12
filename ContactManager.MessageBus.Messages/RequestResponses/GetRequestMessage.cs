using ContactManager.MessageBus.Messages.DataTypes.Enums;
using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Request message for getting data
    /// </summary>
    public sealed class GetRequestMessage : RequestMessageBase
    {
        /// <summary>
        /// Request type, can be by id or by query
        /// </summary>
        public GetRequestType GetReuestType { get; }

        /// <summary>
        /// Query to search data
        /// </summary>
        public string? Query { get; }

        /// <summary>
        /// Id if request is to search by id
        /// </summary>
        public Guid? Id { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="returnAddress">Return address</param>
        /// <param name="getRequestType">Request type</param>
        /// <param name="query">Query</param>
        /// <param name="id">Id</param>
        /// <exception cref="ArgumentException"></exception>
        public GetRequestMessage(Guid requestMessageId, string returnAddress,
            GetRequestType getRequestType, string? query, Guid? id)
            : base(requestMessageId, returnAddress)
        {
            if (query == null && id == null)
                throw new ArgumentException("Both query and id cannot be null");

            GetReuestType = getRequestType;
            Query = query;
            Id = id;
        }
    }
}
