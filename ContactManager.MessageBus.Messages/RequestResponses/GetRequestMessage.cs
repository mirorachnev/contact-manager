using System;

namespace ContactManager.MessageBus.Messages.RequestResponses
{
    /// <summary>
    /// Request message for getting data
    /// </summary>
    public sealed class GetRequestMessage : RequestMessageBase
    {   
        /// <summary>
        /// Id if request is to search by id
        /// If id is null in request, response must return all data
        /// </summary>
        public Guid? Id { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessageId">Id of request message</param>
        /// <param name="returnAddress">Return address</param>
        /// <param name="id">Id</param>
        public GetRequestMessage(Guid requestMessageId, string returnAddress, Guid? id)
            : base(requestMessageId, returnAddress)
        {                        
            Id = id;
        }
    }
}
