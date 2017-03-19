using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using english.Models;
using Microsoft.AspNetCore.Mvc;

namespace english.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        EnglishContext _db;

        public ValuesController(EnglishContext db)
        {
            this._db = db;
        }

        // GET api/values
        [HttpGet]
        public void Get([FromQuery]string phrase)
        {
            _db.Phrases.Add(new Phrases() { Text = phrase });
            _db.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string phrase)
        {
            if (phrase == null || phrase == "")
                throw new InvalidOperationException("empty phrase");

            _db.Phrases.Add(new Phrases() { Text = phrase });
            _db.SaveChanges();
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
