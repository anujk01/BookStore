using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Rating { get; set; }
        public int TotalRating { get; set; }
        public int DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
