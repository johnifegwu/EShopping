

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses
{
    public class TypesResponse
    {
        public ObjectId Id { get; set; } = default!;
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
    }
}
