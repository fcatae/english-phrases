// API
var ankiAPI = (function () {

    var URL_ANKI_API = (api) => `/api/anki/${api}`;
    var APPLICATION_JSON = "application/json";

    var anki = {};

    anki.start = function (user, isFirstLogin) {

        var userInfo = { user: user, isFirstLogin: isFirstLogin};

        return $.ajax({
            type: "POST", 
            url: URL_ANKI_API("start"),
            data: JSON.stringify(userInfo),
            contentType: APPLICATION_JSON        
        });
        // returns (session_id: string)
    }

    anki.question = function (session_id, user) {

        var queryString = {user: user};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/question`),
            data: queryString
        });
    } // returns QuestionResponse { question_id, question_text }

    anki.answer = function (session_id, user, question_id) {

        var queryString = {user: user, question_id: question_id};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/answer`),
            data: queryString
        }); // returns (answer: string)
    }

    anki.rate = function (session_id, user, question_id, rating) {

        var questionRating = { user: user, question_id: question_id, rating: rating};

        return $.ajax({
            type: "POST", 
            url: URL_ANKI_API(`${session_id}/rate`),
            data: JSON.stringify(questionRating),
            contentType: APPLICATION_JSON        
        });
    }

    anki.pendingAnswer = function () {

        return $.ajax({
            type: "POST",
            url: URL_ANKI_API('pendinganswer'),
            contentType: APPLICATION_JSON
        });
    }

    anki.translate = function(question_id, answer_text) {

        var queryString = { question_id: question_id, answer_text: answer_text };

        return $.ajax({
            type: "GET",
            url: URL_ANKI_API('translate'),
            data: queryString
        });
    }

    return anki;

})();
