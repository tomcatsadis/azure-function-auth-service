using System.ComponentModel.DataAnnotations;

namespace AuthService.Persistence.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string DatabaseName { get; set; }

        [Required]
        public string UserCollectionName { get; set; }

        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
