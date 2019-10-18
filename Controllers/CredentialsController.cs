using System.Collections.Generic;
using System.Linq;
using getting_started_with_apollo_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace getting_started_with_apollo_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
       private IDataStaxService Service { get; set; }

        public CredentialsController(IDataStaxService service)
        {
            Service = service;
        }
        [HttpGet]
        public ActionResult CheckConnection()
        {
            try{
                if (Service.Session!=null) {
                    return Ok();
                } else {

                return Unauthorized();
                };
            } catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        // GET api/credentials/test
        [HttpPost("test")]
        public ActionResult TestCredentials([FromQuery]string username, [FromQuery]string password, [FromQuery]string keyspace)
        {
            //Copy the secure connect bundle to a temporary location
            var filePath = Path.GetTempPath() + "/" + Guid.NewGuid() + ".zip";
            var output = System.IO.File.OpenWrite(filePath);
            Request.Body.CopyTo(output);
            output.Close();

            //Now test to see if it works
            var result = Service.TestConnection(username, password, keyspace, filePath);
            if (result.Item1)
            {
                return Ok();
            } else {
                return Unauthorized(result.Item2);
            }
        }

        // GET api/credentials
        [HttpPost]
        public ActionResult SaveCredentials([FromQuery]string username, [FromQuery]string password, [FromQuery]string keyspace)
        {
            //Copy the secure connect bundle to a temporary location
            var filePath = Path.GetTempPath() + "/" + Guid.NewGuid() + ".zip";
            var output = System.IO.File.OpenWrite(filePath);
            Request.Body.CopyTo(output);
            output.Close();

            //Now test to see if it works
            var result = Service.SaveConnection(username, password, keyspace, filePath);
            if (result.Item1)
            {
                return Ok();
            } else {
                return Unauthorized(result.Item2);
            }
        }
    }
}
