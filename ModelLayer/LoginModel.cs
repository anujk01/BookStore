using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class LoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string emailID { get; set; }
        public string password { get; set; }
    }
}
