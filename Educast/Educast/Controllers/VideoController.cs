using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace Educast.Controllers
{
    [Route("api/VideoController")]
    public class VideoController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/VideoStore/" + postedFile.FileName);
                    if (File.Exists(filePath))
                    {
                        bool validor = true;
                        int cont = 1;
                        while (validor)
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/VideoStore/" + cont + postedFile.FileName);
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
                    var filePath = HttpContext.Current.Server.MapPath("~/VideoController/" + postedFile.FileName);
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