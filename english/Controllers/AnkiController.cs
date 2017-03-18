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
        public class UserInfo
        {
            public string user;
            public bool isFirstLogin;
        }

        public class QuestionRating
        {
            public string user;
            public int question_id;
            public int rating;
        }

        [HttpPost("start")]
        public int Start([FromBody]UserInfo userInfo)
        {
            if( userInfo.user == "test" ) 
            {
                return (userInfo.isFirstLogin) ? 50001 : 60009;
            }

            return 0;
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
