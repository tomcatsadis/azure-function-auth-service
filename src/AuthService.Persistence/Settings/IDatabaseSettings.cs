namespace AuthService.Persistence.Settings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }

        string DatabaseName { get; }

        string UserCollectionName { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}
