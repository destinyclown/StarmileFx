using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StarmileFx.Models.MongoDB
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public virtual string Id { get; set; }

    }
}
