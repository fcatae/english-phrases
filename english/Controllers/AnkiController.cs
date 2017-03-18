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

        public class QuestionResponse
        {
            public int question_id;
            public string question_text;
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
        public QuestionResponse Question(string session_id, [FromQuery]string user)
        {
            if( user == "test" ) 
            {
                return new QuestionResponse() { 
                                question_id = 1,
                                question_text = "(test_question) Is this a real test?"
                                };
            }

            return new QuestionResponse();
        }

        [HttpGet("{session_id}/answer")]
        public string Answer(string session_id, [FromQuery]string user, [FromQuery]string question_id)
        {
            if( user == "test" )
            {
                return (question_id == "1") ? 
                            "(test_answer) The answer is correct" :
                            "(test_answer) No, the answer is wrong";
            }

            return "value";
        }

        [HttpPost("{session_id}/rate")]
        public void Rate(int session_id, [FromBody]object questionRating)
        {
        }
    }
}
