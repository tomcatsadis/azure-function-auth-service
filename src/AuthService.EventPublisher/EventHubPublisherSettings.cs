using System.ComponentModel.DataAnnotations;

namespace AuthService.EventPublisher
{
    public class EventHubPublisherSettings : IEventHubPublisherSettings
    {
        [Required]
        public string NewUserEventName { get; set; }

        [Required]
        public string NewUserEventConnectionString { get; set; }

        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}