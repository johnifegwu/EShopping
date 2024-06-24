
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Application.Requests
{
    public class UpdateProductRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductBrandId { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductTypeId { get; set; }
        public decimal Price { get; set; } = default!;
    }
}
