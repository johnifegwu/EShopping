
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Application.Requests
{
    public class UpdateTypeRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        [BsonElement("Name")]
        public string Name { get; set; } = default!;
    }
}
