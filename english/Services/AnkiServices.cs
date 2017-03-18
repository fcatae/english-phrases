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
    }

    class AnkiServices : IAnkiServices
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
    }    
}
