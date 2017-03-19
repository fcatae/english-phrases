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
        int GetPendingAnswer();
        void ProvideAnswer(int question_id, string answer_text);
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
                Phrases phrase = new Phrases() { Text = "I am hungry" };
                Users u = new Users() { Name = "New User " };

                var uq = new UserQuestions() { Phrase = phrase, User = u };

                _db.UserQuestions.Add(uq);
                _db.SaveChanges();
            }

            int total = _db.Phrases.Count();

            var currentUser = _db.Users.First();

            if( total != count )
            {
                int minUserQuestion = _db.UserQuestions.Max(uq => uq.PhraseId);

                var newPhrases = _db.Phrases.Where(p => p.PhraseId > minUserQuestion);

                foreach(var phrase in newPhrases )
                {
                    var newUserQuestion = new UserQuestions() { User = currentUser, Phrase = phrase };

                    _db.UserQuestions.Add(newUserQuestion);
                }
                _db.SaveChanges();
            }

            return 1;
        }

        public int GetRandomQuestion(string user)
        {
            var uq = _db.UserQuestions.OrderByDescending(u => u.Difficulty).First();

            return uq.PhraseId;
        }

        public string GetQuestion(int questionId)
        {
            var qt = _db.Phrases.Find(questionId);

            return qt.Text;
        }

        public string GetAnswer(int questionId)
        {
            var ans = _db.Translations.FirstOrDefault(t => t.PhraseId == questionId);

            if( ans == null )
            {
                return "esta frase ainda não tem tradução";
            }

            return ans.Text;
        }

        public void RateQuestion(int question_id, int rating)
        {
            var uq = _db.UserQuestions.First(q => q.PhraseId == question_id);

            uq.Difficulty = (rating + uq.Difficulty)/2;

            _db.SaveChanges();
        }

        public int GetPendingAnswer()
        {
            var ans = _db.Phrases.FirstOrDefault(p => p.Translations.Count == 0);

            //var ans2 = _db.Phrases.FirstOrDefault(p => _db.Translations.FirstOrDefault( t => t.PhraseId == p.PhraseId) == null );
            
            //var ans3 = _db.Phrases.FirstOrDefault(p => p.Translations.FirstOrDefault() == null);

            //Console.WriteLine($"Ans: ${ans.PhraseId}");
            //Console.WriteLine($"Ans2: ${ans2.PhraseId}");
            //Console.WriteLine($"Ans3: ${ans3.PhraseId}");

            return (ans != null) ? ans.PhraseId : -1;
        }

        public void ProvideAnswer(int question_id, string answer_text)
        {
            var answer = new Translations() { PhraseId = question_id, Text = answer_text };
                
            _db.Translations.Add(answer);
            _db.SaveChanges();
        }

    }
}
