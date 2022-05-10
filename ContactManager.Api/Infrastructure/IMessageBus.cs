using ContactManager.MessageBus.Messages.RequestResponses;

namespace ContactManager.Api.Infrastructure
{
    /// <summary>
    /// Message Bus
    /// </summary>
    public interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Publish message method, it will wait for response
        /// </summary>
        /// <typeparam name="TMessage">Response message</typeparam>
        /// <param name="requestMessage">Request message</param>
        /// <returns>Response</returns>
        Task<TResponseMessage?> PublishMessageAndWaitForResponseAsync<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage) 
            where TRequestMessage : RequestMessageBase
            where TResponseMessage: ResponseMessageBase;

        /// <summary>
        /// Handles response
        /// </summary>
        /// <param name="responseMessage">Response message</param>
        Task HandleResponse(ResponseMessageBase responseMessage);
    }
}
