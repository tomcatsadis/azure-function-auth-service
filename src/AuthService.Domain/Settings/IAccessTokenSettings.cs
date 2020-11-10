using System;

namespace AuthService.Domain.Settings
{
    public interface IAccessTokenSettings
    {
        string PrivateKey { get; }

        string Issuer { get; }

        string Audience { get; }

        uint ExpiresIn { get; }

        DateTime Expires { get; }

        DateTime NotBefore { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}
