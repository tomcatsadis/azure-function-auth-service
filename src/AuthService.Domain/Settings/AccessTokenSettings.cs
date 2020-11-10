using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Settings
{
    public class AccessTokenSettings : IAccessTokenSettings
    {
        [Required]
        public string PrivateKey { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public uint ExpiresIn { get; set; }

        public DateTime Expires
        {
            get
            {
                return NotBefore.AddSeconds(ExpiresIn);
            }
        }

        public DateTime NotBefore
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
