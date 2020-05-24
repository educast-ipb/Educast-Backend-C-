using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educast.Models
{
    public class XMLDoc
    {
        public ObjectId Id { get; set; }
        public BsonDocument XML {get;set;}
    }
}
