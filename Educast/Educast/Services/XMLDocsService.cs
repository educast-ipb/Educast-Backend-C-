using Educast.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educast.Services
{
    public class XMLDocsService
    {
        private readonly IMongoCollection<XMLDoc> _xml;
        public XMLDocsService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Educast");

            _xml = database.GetCollection<XMLDoc>("xmlFiles");
        }

        public List<XMLDoc> Get() =>
            _xml.Find(xml => true).ToList();

        public XMLDoc Get(ObjectId id) =>
            _xml.Find<XMLDoc>(xml => xml.Id == id).FirstOrDefault();

        public XMLDoc Create(XMLDoc xml)
        {
            _xml.InsertOne(xml);
            return xml;
        }

        public void Update(ObjectId id, XMLDoc xmlIn) =>
            _xml.ReplaceOne(xml => xml.Id == id, xmlIn);

        
    }
}
