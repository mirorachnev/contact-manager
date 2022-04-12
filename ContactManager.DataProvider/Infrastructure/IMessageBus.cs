namespace ContactManager.DataProvider.Infrastructure
{
    /// <summary>
    /// Message bus contract
    /// </summary>
    internal interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Connect to message bus
        /// </summary>
        Task ConnectAsync();
    }
}
