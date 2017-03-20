using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using english.Models;
using Microsoft.AspNetCore.Mvc;

namespace english.Controllers
{
    [Route("reset")]
    public class ResetController : Controller
    {
        EnglishContext _db;

        public ResetController(EnglishContext db)
        {
            this._db = db;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            // Delete
            _db.Database.EnsureDeleted();

            // Create 
            _db.Database.EnsureCreated();

            // Populate data
            Startup.SetupDatabase(_db);

            return "Done";
        }
        
    }
}
