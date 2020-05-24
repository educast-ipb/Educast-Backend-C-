using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educast.Models
{
    public class Files
    {
        public ObjectId Id { get; set; }
        public string fileName { get; set; }
        public byte[] file { get; set; }
        public int idXML { get; set; }
    }
}
