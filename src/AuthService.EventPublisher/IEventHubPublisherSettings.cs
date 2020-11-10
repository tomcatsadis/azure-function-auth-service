namespace AuthService.EventPublisher
{
    public interface IEventHubPublisherSettings
    {
        string NewUserEventName { get; }

        string NewUserEventConnectionString { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}