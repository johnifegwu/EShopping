
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Application.Responses
{
    public class ProductResponse
    {
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public ObjectId? ProductBrandId { get; set; }
        public ObjectId? ProductTypeId { get; set; }
        public decimal Price { get; set; } = default!;
    }
}
