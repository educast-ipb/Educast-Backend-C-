using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Driver;
using Educast.Models;
using MongoDB.Bson;

namespace Educast.Controllers
{
    [Route("api/FileController")]
    public class FileController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Upload()
        {
            HttpResponseMessage result;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/FilesStore/" + postedFile.FileName);
                    if (File.Exists(filePath))
                    {
                        bool validor = true;
                        int cont = 1;
                        while (validor)
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/FilesStore/" + cont + postedFile.FileName);
                            if (!File.Exists(filePath))
                            {
                                postedFile.SaveAs(filePath);
                                docfiles.Add(filePath);
                                validor = false;
                            }
                            else
                            {
                                validor = true;
                                cont++;
                            }
                        }
                    }
                    else
                    {
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update()
        {
            HttpResponseMessage result;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/FilesStore/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
