
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public ObjectId? ProductBrandId { get; set; }
        public ObjectId? ProductTypeId { get; set; }
        public decimal Price {  get; set; } = default!;
    }
}
