using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using english.Services;

namespace english.Controllers
{
    [Route("api/[controller]")]
    public class AnkiController : Controller
    {
        private readonly IAnkiServices _ankiServices;

        public AnkiController(IAnkiServices ankiServices)
        {
            this._ankiServices = ankiServices;
        }

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
            return _ankiServices.StartSession(userInfo.user, userInfo.isFirstLogin);
        }

        [HttpGet("{session_id}/question")]
        public QuestionResponse Question(string session_id, [FromQuery]string user)
        {
            int questionId = _ankiServices.GetRandomQuestion(user);
            string questionText = _ankiServices.GetQuestion(questionId);

            return new QuestionResponse() {
                question_id = questionId,
                question_text = questionText
            };
        }

        [HttpGet("{session_id}/answer")]
        public string Answer(string session_id, [FromQuery]string user, [FromQuery]string question_id)
        {
            int questionId = Int32.Parse(question_id);
            
            return _ankiServices.GetAnswer(questionId);
        }

        [HttpPost("{session_id}/rate")]
        public bool Rate(int session_id, [FromBody]QuestionRating questionRating)
        {
            _ankiServices.RateQuestion(questionRating.question_id, questionRating.rating);

            return true;
        }

        [HttpPost("pendinganswer")]
        public QuestionResponse GetPendingAnswer()
        {
            int questionId = _ankiServices.GetPendingAnswer();

            if( questionId == -1 )
            {
                return new QuestionResponse() { question_id = -1, question_text = "" };
            }

            string questionText = _ankiServices.GetQuestion(questionId);

            return new QuestionResponse() { question_id = questionId, question_text = questionText };
        }

        [HttpGet("translate")]
        public void Translate([FromQuery]int question_id, [FromQuery]string answer_text)
        {
            _ankiServices.ProvideAnswer(question_id, answer_text);
        }
    }
}
