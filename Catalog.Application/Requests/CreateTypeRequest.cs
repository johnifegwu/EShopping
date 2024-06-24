
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Requests
{
    public class CreateTypeRequest
    {
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
    }
}
