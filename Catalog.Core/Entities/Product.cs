
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
        public ProductBrand Brands { get; set; } = default!;
        public ProductType Types { get; set; } = default!;

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price {  get; set; } = default!;
    }
}
