using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class WishlistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WishlistID { get; set; }
        [ForeignKey("BookModel")]
        public string BookID { get; set; }
        public virtual BookModel bookModel { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel registerModel { get; set; }
    }
}
