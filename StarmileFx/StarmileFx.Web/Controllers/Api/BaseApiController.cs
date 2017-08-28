using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StarmileFx.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/BaseApi")]
    public class BaseApiController : Controller
    {
        
        //“¿¿µ◊¢»Î
        //private IBaseServer _BaseServer;
        public BaseApiController()
        {
        }

        // GET: api/Api
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Api/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Api
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Api/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
