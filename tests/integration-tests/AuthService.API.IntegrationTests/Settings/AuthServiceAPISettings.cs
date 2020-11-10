using System.ComponentModel.DataAnnotations;

namespace AuthService.API.IntegrationTests.Settings
{
    public class AuthServiceAPISettings
    {
        [Required]
        public string OcpApimSubscriptionKey { get; private set; }

        [Required]
        public string BaseUrl { get; private set; }

        [Required]
        public string ChangePasswordUrl { get; private set; }

        [Required]
        public string SignInByEmailUrl { get; private set; }

        [Required]
        public string SignInByPhoneUrl { get; private set; }

        [Required]
        public string SignUpByEmailUrl { get; private set; }

        [Required]
        public string SignUpByPhoneUrl { get; private set; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
