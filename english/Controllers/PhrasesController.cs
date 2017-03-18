using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace english.Controllers
{
    [Route("api/[controller]")]
    public class PhrasesController : Controller
    {
        List<string> _col = new List<string>();
        public PhrasesController()
        {
            _col.AddRange(new string[] { "the boy and the girl are studying" , "I want to rock" });
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _col.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _col[id];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            if( value == null || value == "" )
                throw new InvalidOperationException("value is null or empty");
                
            _col.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
