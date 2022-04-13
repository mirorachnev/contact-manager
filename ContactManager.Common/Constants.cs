namespace ContactManager.Common
{
    /// <summary>
    /// Shared constants
    /// </summary>
    public class Constants
    {
        // Message bus connection string
        public const string RabbitMqConnectionString = "amqp://guest:guest@localhost:5672";

        // Name of message queue
        public const string RabbitMqQueueName = "ContactManagerQueue";

        // Database connection 
        public const string ContactManagerDbConnectionStringName = "ContactManagerDbConnectionString";

        // Api service return address
        public const string ApiServiceReturnAddress = "ApiServiceReturnAddress";
    }
}
