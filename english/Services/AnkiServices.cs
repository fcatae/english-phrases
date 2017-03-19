using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using english.Models;

namespace english.Services
{
    public interface IAnkiServices 
    {
        int StartSession(string user, bool isFirstLogin);
        int GetRandomQuestion(string user);
        string GetQuestion(int question_id);
        string GetAnswer(int question_id);
        void RateQuestion(int question_id, int rating);
    }

    public class AnkiServices : IAnkiServices
    {
        EnglishContext _db;

        public AnkiServices(EnglishContext db)
        {            
            this._db = db;
        }

        public int StartSession(string user, bool isFirstLogin)
        {
            int count = _db.UserQuestions.Count();

            if( count == 0 )
            {
                Phrases phrase = new Phrases() { Text = "New phrase" };
                Users u = new Users() { Name = "New user " };

                var uq = new UserQuestions() { Phrase = phrase, User = u };

                _db.UserQuestions.Add(uq);
                _db.SaveChanges();


            }

            var a = _db;
            return 1;
        }

        public int GetRandomQuestion(string user)
        {
            var uq = _db.UserQuestions.OrderByDescending(u => u.Difficulty).First();

            return uq.PhraseId;
        }

        public string GetQuestion(int questionId)
        {
            var p = _db.Phrases.Find(questionId);

            return p.Text;
        }

        public string GetAnswer(int questionId)
        {
            var ans = _db.Translations.FirstOrDefault(t => t.PhraseId == questionId);

            if( ans == null )
            {
                ans = new Translations() { PhraseId = questionId, Text = "esta frase ainda não tem tradução" };
                _db.Translations.Add(ans);
                _db.SaveChanges();
            }

            return ans.Text;
        }

        public void RateQuestion(int question_id, int rating)
        {
            var uq = _db.UserQuestions.First(q => q.PhraseId == question_id);

            uq.Difficulty = rating - 1;

            _db.SaveChanges();
        }

    }
    class TestAnkiServices : IAnkiServices
    {
        public int StartSession(string user, bool isFirstLogin)
        {
            if( user == "test" ) 
            {
                return (isFirstLogin) ? 50001 : 60009;
            }

            return -1;
        }

        public int GetRandomQuestion(string user)
        {
            int INVALID_QUESTION_ID = -1;

            return ( user == "test" ) ? 123 : INVALID_QUESTION_ID;
        }

        public string GetQuestion(int questionId)
        {
            switch(questionId)
            {
                case 123: 
                    return "(test_question) Is this a real test?";
                case -1:
                    return "who are you?";                
            }
            
            return "what is your question?";
        }

        public string GetAnswer(int questionId)
        {
            switch(questionId)
            {
                case 123: 
                    return "(test_answer) The answer is correct";
                case -1:
                    return "negative value - very weird question";
            }
            
            return "(test_answer) No, the answer is wrong";
        }

        public void RateQuestion(int question_id, int rating)
        {
            if( question_id == 123 && rating >= 1 && rating <= 100 )
            {                
            }
            else throw new InvalidOperationException("failed test");
        }

    }    
}
