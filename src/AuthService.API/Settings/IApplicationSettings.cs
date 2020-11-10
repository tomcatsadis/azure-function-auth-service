namespace AuthService.API.Settings
{
    public interface IApplicationSettings
    {
        string HelloWorldMessage { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}
