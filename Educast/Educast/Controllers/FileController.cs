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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace Educast.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FileController : ApiController
    {
        private readonly IMongoCollection<Files> _files;

        public FileController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Educast");

            _files = database.GetCollection<Files>("files");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();


            }

            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<HttpResponseMessage> Update(ObjectId id)
        {
            try
            {
                
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
