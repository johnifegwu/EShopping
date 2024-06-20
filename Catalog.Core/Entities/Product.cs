
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public ObjectId? ProductBrandId { get; set; }
        public ObjectId? ProductTypeId { get; set; }
        public decimal Price {  get; set; } = default!;
    }
}
