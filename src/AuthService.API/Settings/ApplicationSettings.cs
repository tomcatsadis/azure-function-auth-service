using System.ComponentModel.DataAnnotations;

namespace AuthService.API.Settings
{
    class ApplicationSettings : IApplicationSettings
    {
        [Required]
        public string HelloWorldMessage { get; set; }

        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
