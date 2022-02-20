using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoopBreakers.ReportModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityReportController : ControllerBase
    {
        // GET: api/<ActivityReportController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ActivityReportController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActivityReportController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActivityReportController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActivityReportController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
