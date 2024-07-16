
using MongoDB.Bson;

namespace Catalog.Core.Entities
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
