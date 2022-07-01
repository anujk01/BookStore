using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class AddressModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string AddressID { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel registerModel { get; set; }

        [ForeignKey("AddressType")]
        public string AddressTypeID { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double PinCode { get; set; }
    }
}
