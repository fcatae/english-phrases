
# Ideias para o Service Fabric

Comparando o código:

```
    class AnkiObject
    {
        private int _questionId;

        public static AnkiObject StartSession(string user, bool isFirstLogin)
        {      
        }

        public string GetQuestion()
        {
        }
        
        public string GetAnswer()
        {
        }

        void RateQuestion(int rating)
        {
        }
    }
```

Com a interface:

```
    public interface IAnkiServices 
    {
        int StartSession(string user, bool isFirstLogin);
        int GetRandomQuestion(string user);
        string GetQuestion(int question_id);
        string GetAnswer(int question_id);
        void RateQuestion(int question_id, int rating);
    }
```

Ilustra a diferença clara entre um código com orientação à objeto e um 
código com função.