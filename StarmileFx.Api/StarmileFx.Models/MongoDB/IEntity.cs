using MongoDB.Bson.Serialization.Attributes;

namespace StarmileFx.Models.MongoDB
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}
