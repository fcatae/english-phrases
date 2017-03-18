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
    }

    anki.question = function (session_id, user) {

        var queryString = {user: user};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/question`),
            data: queryString
        });
    }

    anki.answer = function (session_id, user, question_id) {

        var queryString = {user: user, question_id: question_id};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/answer`),
            data: queryString
        });
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

    return anki;

})();
