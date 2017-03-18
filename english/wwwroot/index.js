
// - start session
// - get question
// - show answer key
// - rate the question
// - restart

// jQuery Controller
$(function () {

    $('#btn_answer').click( ()=> { alert('show answer') });
    $('#question_text').text('The boy and the girl are studying today');
    $('#answer_text').text('O menino e a menina estão estudando hoje');
    $('.answerrating').click( (ev)=> { 
        let button = ev.target
        alert('rate: ' + button.value) });

});

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

// start tests
$(function() {
    testAnki( ankiAPI );
})

// test functions
function testAnki(anki) {

    // Tests: start
    anki.start('test', true).done( v => {
        var START_TEST_TRUE = 50001;        
        if(v != START_TEST_TRUE) 
            alert('/api/anki/start FAILED')
    });

    anki.start('test', false).done( v => {
        var START_TEST_FALSE = 60009;
        if(v != START_TEST_FALSE) 
            alert('/api/anki/start FAILED')
    });

    // Tests: question
    anki.question(50001, 'test').done( v => {
        var QUESTION_TEST = '(test_question)';

        var valid = (v) && 
                    (v.question_text) &&
                    (v.question_text.startsWith(QUESTION_TEST));

        if(!valid) 
            alert('/api/anki/question FAILED');  
    });

    // Tests: answer
    anki.answer(50001, 'test', 1).done( v => {
        var ANSWER_TEST = '(test_answer)';

        if(!v.startsWith(ANSWER_TEST)) 
            alert('/api/anki/answer FAILED');
    });

    // Tests: rate
    anki.rate(50001, 'test', 123, 100).done( ret => {
        if(ret == false)
            alert(ret);
    });
}
