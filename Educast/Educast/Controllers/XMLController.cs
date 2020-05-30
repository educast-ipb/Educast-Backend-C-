using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Driver;
using Educast.Models;
using MongoDB.Bson;
using System.Xml;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Educast.Controllers
{
    [Route("api/XMLController")]
    [ApiController]
    public class XMLController: ControllerBase
    {
        private readonly IMongoCollection<XMLDoc> _xml;

        public XMLController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Educast");

            _xml = database.GetCollection<XMLDoc>("xmlFiles");
        }

        [HttpPost]
        public async Task<XMLDoc> Create()
        {
            try
            {
                XMLDoc xml = new XMLDoc();
                using (var reader = new StreamReader(HttpContext.Request.Body))
                {
                    var body = reader.ReadToEnd(); // read input string
                    
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(body); // String to XML Document

                    string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                    var bsdocument = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document
                    xml.XML = bsdocument;
                    await _xml.InsertOneAsync(xml); //Insert into mongoDB
                }
                return xml;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        [HttpPut]
        public async Task<XMLDoc> Update(ObjectId id)
        {
            try
            {
                XMLDoc xmlIn = _xml.Find(xml => xml.Id == id).First();
                using (var reader = new StreamReader(HttpContext.Request.Body))
                {
                    var body = reader.ReadToEnd(); // read input string
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(body); // String to XML Document

                    string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                    var bsdocument = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document
                    xmlIn.XML = bsdocument;
                    await _xml.ReplaceOneAsync(xml => xml.Id == id, xmlIn); 
                }
                return xmlIn;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
    
}
