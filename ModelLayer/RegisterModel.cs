﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class RegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string userID { get; set; }
        public string FullName { get; set; }

        [Required]
        public string emailID { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }
    }
}
