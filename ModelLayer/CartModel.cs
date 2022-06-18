using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class CartModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string CartID { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel registerModel { get; set; }

        [ForeignKey("BookModel")]
        public string BookID { get; set; }
        public virtual BookModel bookModel { get; set; }
        public int Quantity { get; set; }
    }
}
