
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public ObjectId? BrandId { get; set; }
        public ProductBrand Brands { get; set; } = default!;
        public ObjectId? TypeId { get; set; }
        public ProductType Types { get; set; } = default!;
        public decimal Price {  get; set; } = default!;
    }
}
