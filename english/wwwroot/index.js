
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
    var URL_ANKI_API = (api) => baseAddress + "/api/anki/" + api;
    var APPLICATION_JSON = "application/json";

    var POST = "POST";
    var URL_ANKI_START = baseAddress + "/api/anki/start";

    var start = function (user, isFirstLogin) {

        var userInfo = { user: user, isFirstLogin: isFirstLogin};

        $.ajax({
            type: "POST", 
            url: URL_ANKI_API("start"),
            data: JSON.stringify(userInfo),
            contentType: APPLICATION_JSON
        });

    }

    start('test', true);

});
