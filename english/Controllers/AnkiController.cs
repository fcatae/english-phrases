using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace english.Controllers
{
    [Route("api/[controller]")]
    public class AnkiController : Controller
    {
        [HttpPost("start")]
        public void Start([FromBody]object userInfo)
        {
        }

        [HttpGet("{session_id}/question")]
        public string Question(string session_id, string user)
        {
            return "";
        }

        [HttpGet("{session_id}/answer")]
        public string Answer(string session_id, int question_id)
        {
            return "value";
        }

        [HttpPost("{session_id}/rate")]
        public void Rate(int session_id, [FromBody]object questionRating)
        {
        }
    }
}
