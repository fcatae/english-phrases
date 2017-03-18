using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

    class AnkiServices 
    {
        public int StartSession(string user, bool isFirstLogin)
        {
            return 0;
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
        public string GetAnswer(int question_id)
        {
            throw new NotImplementedException();
        }

        public string GetQuestion(int question_id)
        {
            throw new NotImplementedException();
        }

        public int GetRandomQuestion(string user)
        {
            throw new NotImplementedException();
        }

        public void RateQuestion(int question_id, int rating)
        {
            throw new NotImplementedException();
        }

    }    
}
