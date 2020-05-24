using Educast.Models;
using Educast.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Educast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XMLController: ControllerBase
    {
        private readonly XMLDocsService _xmldocsService;
        private readonly IMongoCollection<XMLDoc> _xml;

        public XMLController(XMLDocsService xmldocsService)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Educast");

            _xml = database.GetCollection<XMLDoc>("xmlFiles");
            _xmldocsService = xmldocsService;
        }


        [HttpPost]
        public async Task<ActionResult> Create()
        {
            try
            {
                using (var reader = new StreamReader(HttpContext.Request.Body))
                {
                    var body = reader.ReadToEnd(); // read input string
                    XMLDoc xml = new XMLDoc();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(body); // String to XML Document

                    string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                    var bsdocument = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document
                    xml.XML = bsdocument;
                    await _xml.InsertOneAsync(xml); //Insert into mongoDB
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ObjectId id)
        {
            try
            {
                using (var reader = new StreamReader(HttpContext.Request.Body))
                {
                    var body = reader.ReadToEnd(); // read input string
                    XMLDoc xmlIn = _xml.Find(xml => xml.Id == id).First();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(body); // String to XML Document

                    string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                    var bsdocument = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document
                    xmlIn.XML = bsdocument;
                    _xml.ReplaceOne(xml => xml.Id == id, xmlIn); 
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
    
}
