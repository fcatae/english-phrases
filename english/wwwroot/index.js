
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
$(function () {

    var baseAddress = "http://localhost:5000";
    var URL_ANKI_API = (api) => `${baseAddress}/api/anki/${api}`;
    var APPLICATION_JSON = "application/json";

    var POST = "POST";
    var URL_ANKI_START = baseAddress + "/api/anki/start";

    var start = function (user, isFirstLogin) {

        var userInfo = { user: user, isFirstLogin: isFirstLogin};

        return $.ajax({
            type: "POST", 
            url: URL_ANKI_API("start"),
            data: JSON.stringify(userInfo),
            contentType: APPLICATION_JSON        
        }).then( ret => {
            return JSON.parse(ret);
        });
    }

    var question = function (session_id, user) {

        var queryString = {user: user};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/question`),
            data: queryString
        }).then( question_text => {
            return question_text;
        });
    }

    var answer = function (session_id, user) {

        var queryString = {user: user};

        return $.ajax({
            type: "GET", 
            url: URL_ANKI_API(`${session_id}/question`),
            data: queryString
        }).then( question_text => {
            return question_text;
        });
    }

    // Tests: start
    start('test', true).done( v => {
        var START_TEST_TRUE = 50001;        
        if(v != START_TEST_TRUE) 
            alert('/api/anki/start FAILED')
    });

    start('test', false).done( v => {
        var START_TEST_FALSE = 60009;
        if(v != START_TEST_FALSE) 
            alert('/api/anki/start FAILED')
    });

    // Tests: question
    question(50001, 'test').done( v => {
        var QUESTION_TEST = '(test)';

        if(!v.startsWith(QUESTION_TEST)) 
            alert('/api/anki/question FAILED')    });

});
