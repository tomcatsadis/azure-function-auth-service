using MongoDB.Bson.Serialization.Attributes;

namespace AuthService.Persistence.Entities
{
    public class Claim
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }

        public Domain.Entities.Claim MapToDomainEntity()
        {
            return new Domain.Entities.Claim(
                type: Type,
                value: Value);
        }

        public static Claim Load(Domain.Entities.Claim claim)
        {
            if (claim == null) return null;

            return new Claim
            {
                Type = claim.Type,
                Value = claim.Value,
            };
        }
    }
}
