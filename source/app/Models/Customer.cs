using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace app.Models
{
    public class Customer
    {
        [BsonId]
        public Guid CustomerId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
    }
}